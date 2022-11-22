using Microsoft.Extensions.Options;
using RedisLab.RedisLibrary.Contexts.StackExchangeRedisContext;
using RedisLab.RedisLibrary.Interfaces;
using RedisLab.RedisLibrary.Model;
using RedisLab.RedisLibrary.Option;
using StackExchange.Redis;

namespace RedisLab.RedisLibrary
{
    internal sealed class RedisManager : IRedisManager
    {
        private readonly Dictionary<string, IRedisConnection> _connections;

        public RedisManager(IOptions<RedisOption> options)
        {
            _connections = new Dictionary<string, IRedisConnection>();

            var redisConfiguration = options.Value;
            foreach (var config in redisConfiguration)
            {
                AddGroupRedis(config.Key, config.Value);
            }
        }

        private void AddGroupRedis(string groupName, RedisGroupOption redisGroupOption)
        {
            if (HaveGroup(groupName))
            {
                throw new ArgumentException($"An element with the same key({groupName}) already exists");
            }
            var connection = new StackExchangeRedisConnection(redisGroupOption);
            _connections.Add(groupName, connection);
        }

        private bool HaveGroup(string groupName)
        {
            return _connections.ContainsKey(groupName);
        }

        IDatabase IRedisManager.GetDatabase(string groupName)
        {
            ThrowIfNoExistGroup(groupName);
            return _connections[groupName].GetDatabase();
        }

        IEnumerable<RedisEndPoint> IRedisManager.GetEndPoint(string groupName)
        {
            ThrowIfNoExistGroup(groupName);
            return _connections[groupName].GetRedisMessage();
        }

        private void ThrowIfNoExistGroup(string groupName)
        {
            if (!HaveGroup(groupName))
            {
                throw new ArgumentNullException(nameof(groupName));
            }
        }

        ISubscriber IRedisManager.GetSubscriber(string groupName)
        {
            ThrowIfNoExistGroup(groupName);
            return _connections[groupName].GetSubscriber();
        }
    }
}
