using StackExchange.Redis;

namespace RedisLab.Interfaces
{
    public interface IRedisAccessor
    {
        Task LockTakeAsync(string key, RedisValue token);
        Task<T> ObjectGetAsync<T>(string key);
        Task<bool> KeyDeleteAsync(string key);
        Task<bool> KeyExistAsync(string key);
        Task PublishAsync(string groupName, string channel, string key);
    }
}
