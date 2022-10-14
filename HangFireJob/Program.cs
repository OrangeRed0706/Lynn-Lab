using Hangfire;
using HangFireJob.Interface;
using HangFireJob.Job;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddSingleton<IExampleJob, ExampleJob>();

services.AddHangfire((configuration) =>
{
    configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
    configuration.UseRedisStorage("127.0.0.1:6379", new Hangfire.Redis.RedisStorageOptions
    {
        Prefix = "hangfire:",
        Db = 1
    });
});
services.AddHangfireServer(options =>
{
    options.Queues = new string[] { "critical", "default" };
    RecurringJob.AddOrUpdate<IExampleJob>("Example", job => job.Run(), Cron.Minutely);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();

app.Run();
