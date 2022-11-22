using RedisLab.RedisLibrary.Model;
using StackExchange.Redis;

namespace RedisLab.RedisLibrary.Interfaces
{
    public interface IRedisManager
    {
        IDatabase GetDatabase(string groupName);
        ISubscriber GetSubscriber(string groupName);
        IEnumerable<RedisEndPoint> GetEndPoint(string groupName);
    }
}
