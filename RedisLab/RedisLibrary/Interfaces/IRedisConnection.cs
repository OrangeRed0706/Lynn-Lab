using RedisLab.RedisLibrary.Model;
using StackExchange.Redis;

namespace RedisLab.RedisLibrary.Interfaces
{
    public interface IRedisConnection
    {
        IDatabase GetDatabase();
        ISubscriber GetSubscriber();
        IEnumerable<RedisEndPoint> GetRedisMessage();
    }
}
