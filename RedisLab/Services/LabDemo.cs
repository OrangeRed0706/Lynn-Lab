using RedisLab.Accessors;
using RedisLab.Interfaces;

namespace RedisLab.Services
{
    public class LabDemo
    {
        private readonly IRedisAccessor _redisAccessor;
        private const string Key = "Hello";
        public LabDemo(IRedisAccessor redisAccessor)
        {
            _redisAccessor = redisAccessor;
        }

        public async Task Run()
        {
            var tasks = new List<Task>();

            for (var i = 0; i < 5; i++)
            {
                var index = i;
                tasks.Add(Task.Run(async () =>
                {
                    var isLock = await _redisAccessor.LockAsync(Key, index, expiry: TimeSpan.FromSeconds(100));
                    if (isLock)
                    {
                        //var target = await _redisAccessor.GetStringAsync($"Lock_{Key}");
                        var testObj = await _redisAccessor.ObjectGetAsync<testClass>($"Lock_{Key}");
                        //Console.WriteLine(testObj);
                        Console.WriteLine($"Thread Id :{Thread.CurrentThread.ManagedThreadId}，拿到Value {testObj.QueryTime}");
                        Console.WriteLine("===========================================================================");

                        //await _redisAccessor.LockReleaseAsync(Key, JsonConvert.SerializeObject(testObj));
                        //Console.WriteLine($"Thread Id :{Thread.CurrentThread.ManagedThreadId}，Key 已被釋放");
                    }
                }));
            }
            await Task.WhenAll(tasks);
            Console.WriteLine("Done.");
        }
    }
}
