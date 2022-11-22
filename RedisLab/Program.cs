
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RedisLab.Accessors;
using RedisLab.Interfaces;
using RedisLab.RedisLibrary;
using RedisLab.Services;

var host = CreateHostBuilder(args).Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var labDemo = services.GetRequiredService<LabDemo>();

    await labDemo.Run();

}
host.Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            services.AddRedisManager(hostContext.Configuration);
            services.AddSingleton<IRedisAccessor, RedisAccessor>();
            services.AddSingleton<LabDemo>();
        }).ConfigureAppConfiguration(config =>
        {
            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        });

