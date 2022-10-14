using HangFireJob.Interface;

namespace HangFireJob.Job;

public class ExampleJob : IExampleJob
{
    public ExampleJob()
    {

    }
    async Task IExampleJob.Run()
    {
        Console.WriteLine("Hello World!");
        await Task.Delay(1000);
    }
}