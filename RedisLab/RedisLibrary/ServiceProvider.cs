using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RedisLab.RedisLibrary.Interfaces;
using RedisLab.RedisLibrary.Option;

namespace RedisLab.RedisLibrary
{
    public static class ServiceProvider
    {
        public static void AddRedisManager(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<RedisOption>(configuration);
            services.TryAddSingleton<IRedisManager, RedisManager>();
        }
    }
}
