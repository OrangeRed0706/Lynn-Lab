using RedisLab.Interfaces;
using System.Collections.Concurrent;

namespace RedisLab.Services
{
    public class TicketService
    {
        private readonly IRedisAccessor _redisAccessor;
        private const string EventCountKey = "Event_Count";

        public TicketService(IRedisAccessor redisAccessor)
        {
            _redisAccessor = redisAccessor;
        }

        public async Task Init()
        {
            //await _redisAccessor.SetStringAsync(EventCountKey, 100);
            //await _redisAccessor.GetStringAsync(EventCountKey);
        }

        public async Task GoSnapUp()
        {
            var result = new ConcurrentStack<bool>();
            var tasks = new List<Task>();

            // 發出105個task
            for (var index = 0; index < 105; index++)
            {
                var number = index;
                tasks.Add(Task.Run(async () =>
                {
                    // 透過 TicketService 處理搶票的邏輯，返回bool
                    result.Push(await GetTicketAsync(EventCountKey));
                    Console.WriteLine($"{number}");
                }));
            }
            await Task.WhenAll(tasks);

            // 驗證拿到成功的client request數量
            Console.WriteLine($"success count: {result.Count(r => r == true)}");
        }

        private async Task<bool> GetTicketAsync(string key)
        {
            // 只有在數量還有剩 且 透過Redis的Lock成功，才繼續搶票的動作
            // 這邊Lock的Timeout時間為100毫秒，純粹只是為了測試
            if (await TicketCountAsync(key) > 0 && await _redisAccessor.LockAsync(key, TimeSpan.FromMilliseconds(2000)))
            {
                try
                {
                    // 遞減數量，會返回剩餘的數量，剩餘數量小於0代表超賣了，會返回失敗
                    var lastCount = await _redisAccessor.StringDecrementAsync(key);

                    return lastCount >= 0;
                }
                finally
                {
                    // 完成後要把Lock釋放
                    await _redisAccessor.LockReleaseAsync(key, Environment.MachineName);
                }
            }

            return false;
        }

        private async Task<int> TicketCountAsync(string key)
        {
            return (int)await _redisAccessor.GetStringAsync(key);
        }
    }
}
