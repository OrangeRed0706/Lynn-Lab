using StackExchange.Redis;

namespace RedisLab.Interfaces
{
    public interface IRedisAccessor
    {
        Task<bool> LockAsync(string key, TimeSpan expiry);
        Task<bool> LockAsync(string key, RedisValue redisValue, TimeSpan expiry);
        Task<bool> SetStringAsync(string key, RedisValue value);
        Task<RedisValue> GetStringAsync(string key);
        Task<bool> LockReleaseAsync(string key, RedisValue value);
        Task<long> StringDecrementAsync(string key);
        Task<T?> ObjectGetAsync<T>(string key);

    }
}
