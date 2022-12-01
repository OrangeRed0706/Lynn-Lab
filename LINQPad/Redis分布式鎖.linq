<Query Kind="Program">
  <NuGetReference>StackExchange.Redis</NuGetReference>
  <Namespace>StackExchange.Redis</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async Task Main()
{
	RedisDbFactory.Init("127.0.0.1:6379");
	var demoService = new DemoService(RedisDbFactory.Instance);
	var users = new string[5] { "UserA", "UserB", "UserC", "UserD", "UserE" };
	var tasks = new List<Task>();
	foreach (var user in users)
	{
		tasks.Add(Task.Run(async () =>
		{
			await demoService.GetLockAsync("Hello",user);
		}));
	}
	await Task.WhenAll(tasks);
}

public sealed class RedisDbFactory
{
	public static IDatabase Instance
	{
		get { return _connection.Value.ConnectionMultiplexer.GetDatabase(); }
	}
	public readonly ConnectionMultiplexer ConnectionMultiplexer;
	private static readonly Lazy<RedisDbFactory> _connection = new Lazy<RedisDbFactory>(() =>
	{
		return new RedisDbFactory();
	});
	private static string _connectionString;

	private RedisDbFactory()
	{
		ConnectionMultiplexer = ConnectionMultiplexer.Connect(_connectionString);
	}

	public static void Init(string connectionString)
	{
		_connectionString = connectionString;
	}
}
public sealed class DemoService
{
	private readonly IDatabase _redisdb;
	private const string value = "Hello World";
	public DemoService(IDatabase redisdb)
	{
		_redisdb = redisdb;
	}

	public async Task GetLockAsync(string key, string user)
	{
		if (await _redisdb.LockTakeAsync(key, value, TimeSpan.FromSeconds(10)))
		{
			Console.WriteLine($"Thread Id:{Thread.CurrentThread.ManagedThreadId}, {user} 成功取得鎖");
			try
			{
				Console.WriteLine("做些什麼......");
			}
			finally
			{
				await _redisdb.LockReleaseAsync(key, value);
				Console.WriteLine("鎖已被釋放！");
				Console.WriteLine("====================================================================");
			}
		}
		else
		{
			Console.WriteLine($"Thread Id:{Thread.CurrentThread.ManagedThreadId}, {user} 沒能取得鎖，稍後重試");
			await Task.Delay(1000);
			await GetLockAsync(key, user);
		}
	}
}