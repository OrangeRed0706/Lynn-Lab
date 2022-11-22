using RedisLab.RedisLibrary.Interfaces;
using RedisLab.RedisLibrary.Model;
using RedisLab.RedisLibrary.Option;
using StackExchange.Redis;

namespace RedisLab.RedisLibrary.Contexts.StackExchangeRedisContext
{
    internal sealed class StackExchangeRedisConnection : IRedisConnection
    {
        private readonly Lazy<ConnectionMultiplexer> _connection;
        private readonly RedisGroupOption _redisConfig;
        public StackExchangeRedisConnection(RedisGroupOption redisConfig)
        {
            _redisConfig = redisConfig;

            var config = new ConfigurationOptions
            {
                AllowAdmin = _redisConfig.AllowAdmin,
                ConnectRetry = _redisConfig.ConnectRetry,
                ConnectTimeout = _redisConfig.ConnectTimeout,
                SyncTimeout = _redisConfig.SyncTimeout,
                ConfigCheckSeconds = _redisConfig.ConfigCheckSeconds,
                Password = _redisConfig.Password,
                Ssl = _redisConfig.Ssl,
                AbortOnConnectFail = _redisConfig.AbortConnect,
                DefaultDatabase = _redisConfig.DefaultDatabase,
            };

            foreach (var endPoint in _redisConfig.EndPoints?.Split(',')!)
            {
                config.EndPoints.Add(endPoint);
            }

            _connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(config));
        }

        IDatabase IRedisConnection.GetDatabase()
        {
            return _connection.Value.GetDatabase();
        }

        IEnumerable<RedisEndPoint> IRedisConnection.GetRedisMessage() =>
            from a in _redisConfig.EndPoints?.Split(',')
            let endPoint = a.Split(':')
            let ip = endPoint[0]
            let port = int.Parse(endPoint[1])
            select new RedisEndPoint
            {
                Ip = ip,
                Port = port
            };

        ISubscriber IRedisConnection.GetSubscriber()
        {
            return _connection.Value.GetSubscriber();
        }
    }
}
