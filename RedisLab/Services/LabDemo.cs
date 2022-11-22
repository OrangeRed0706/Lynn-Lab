using RedisLab.Interfaces;

namespace RedisLab.Services
{
    public class LabDemo
    {
        private readonly IRedisAccessor _redisAccessor;

        public LabDemo(IRedisAccessor redisAccessor)
        {
            _redisAccessor = redisAccessor;
        }

        public async Task Run()
        {

            await _redisAccessor.LockTakeAsync("Hello", 10);
        }

    }
}
