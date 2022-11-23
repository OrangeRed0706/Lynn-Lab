using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RedisLab.Interfaces;
using RedisLab.RedisLibrary.Interfaces;
using StackExchange.Redis;

namespace RedisLab.Accessors
{
    internal class RedisAccessor : IRedisAccessor
    {
        private readonly IRedisManager _redisManager;
        private readonly ILogger<RedisAccessor> _logger;

        public RedisAccessor(IRedisManager redisManager, ILogger<RedisAccessor> logger)
        {
            _redisManager = redisManager;
            _logger = logger;
        }

        public async Task<bool> LockAsync(string key, TimeSpan expiry)
        {
            // Lock失敗就等200毫秒，再重試，最多10次
            var lockKey = $"Lock_{key}";
            var number = 0;
            do
            {
                try
                {
                    GetDatabase(out var db);
                    if (await db.LockTakeAsync(lockKey, Environment.MachineName, expiry))
                    {
                        return true;
                    }
                    await Task.Delay(2000);
                }
                catch (Exception)
                {
                    await Task.Delay(200);
                    number++;
                }
            } while (number < 10);

            return false;
        }
        public async Task<bool> LockAsync(string key, RedisValue redisValue, TimeSpan expiry)
        {
            // Lock失敗就等200毫秒，再重試，最多10次
            var lockKey = $"Lock_{key}";
            var number = 0;
            do
            {
                try
                {
                    GetDatabase(out var db);
                    var a = new testClass()
                    {
                        MachineName = Environment.MachineName,
                        QueryTime = (int)redisValue
                    };

                    if (await db.LockTakeAsync(lockKey, JsonConvert.SerializeObject(a), expiry))
                    {
                        return true;
                    }
                    //if (await db.LockTakeAsync(lockKey, $"{Environment.MachineName}:{redisValue}", expiry))
                    //{
                    //    return true;
                    //}

                    Console.WriteLine($"Thread Id :{Thread.CurrentThread.ManagedThreadId}，Key 已被鎖住");
                    await Task.Delay(2000);
                }
                catch (Exception)
                {
                    Console.WriteLine($"已被Lock住，目前Retry數量：{number}");
                    await Task.Delay(2000);
                    number++;
                }

            } while (number < 5);

            return false;
        }

        public async Task<bool> SetStringAsync(string key, RedisValue value)
        {
            GetDatabase(out var db);
            return await db.StringSetAsync(key, value);
        }

        public async Task<RedisValue> GetStringAsync(string key)
        {
            GetDatabase(out var db);
            return await db.StringGetAsync(key);
        }

        public async Task<bool> LockReleaseAsync(string key, RedisValue value)
        {
            GetDatabase(out var db);
            var lockKey = $"Lock_{key}";
            return await db.LockReleaseAsync(lockKey, value);
        }

        public async Task<long> StringDecrementAsync(string key)
        {
            GetDatabase(out var db);
            return await db.StringDecrementAsync(key);

        }
        public async Task<T?> ObjectGetAsync<T>(string key)
        {
            GetDatabase(out var db);
            var redisValue = await db.StringGetAsync(key).ConfigureAwait(false);
            if (redisValue.HasValue)
            {
                return JsonConvert.DeserializeObject<T>(redisValue.ToString());
            }
            else
            {
                return default;
            }
        }
        private void GetDatabase(out IDatabase db)
        {
            db = _redisManager.GetDatabase("Product");
        }
    }

    internal class testClass
    {
        public string MachineName { get; set; }
        public int QueryTime { get; set; }
        public DateTimeOffset Time { get; set; } = DateTimeOffset.UtcNow.AddHours(8);
    }
}
