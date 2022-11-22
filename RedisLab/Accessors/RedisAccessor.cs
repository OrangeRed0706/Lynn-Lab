using Microsoft.Extensions.Logging;
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

        public async Task LockTakeAsync(string key, RedisValue token)
        {
            var db = _redisManager.GetDatabase("Product");
            _logger.LogInformation("LockTakeAsync: {key} {token}", key, token);
            await db.SetAddAsync(key, token);
            await db.LockTakeAsync(key, token, TimeSpan.FromSeconds(10));
        }

        public async Task<T> ObjectGetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> KeyDeleteAsync(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> KeyExistAsync(string key)
        {
            throw new NotImplementedException();
        }

        public async Task PublishAsync(string groupName, string channel, string key)
        {
            throw new NotImplementedException();
        }
    }
}
