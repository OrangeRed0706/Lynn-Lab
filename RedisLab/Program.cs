
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
    //var ticketService = services.GetRequiredService<TicketService>();
    //await ticketService.Init();
    //await ticketService.GoSnapUp();
    var labDemo = services.GetRequiredService<LabDemo>();
    await labDemo.Run();
}
host.Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args).ConfigureAppConfiguration(config =>
        {
            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        })
        .ConfigureServices((hostContext, services) =>
        {
            services.AddRedisManager(hostContext.Configuration.GetSection("Redis"));
            services.AddSingleton<IRedisAccessor, RedisAccessor>();
            services.AddSingleton<LabDemo>();
            services.AddSingleton<TicketService>();
        });

