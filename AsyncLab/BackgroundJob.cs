using Microsoft.Extensions.Hosting;
using System.Text;

namespace AsyncLab
{
    internal sealed class BackgroundJob : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(() =>
            {
                Console.WriteLine(
                    $"1 Time :{DateTime.Now} , Thread {Thread.CurrentThread.ManagedThreadId} - Hello World!, is thread pool: {Thread.CurrentThread.IsThreadPoolThread}");
            }, stoppingToken);

            Console.WriteLine("BackgroundJob ExecuteAsync");
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine(
                    $"2 Time :{DateTime.Now} , Thread {Thread.CurrentThread.ManagedThreadId} - Hello World!, is thread pool: {Thread.CurrentThread.IsThreadPoolThread}");
                Task.Delay(10000);
                Thread.Sleep(1000);
                Console.WriteLine(
                    $"3 Time :{DateTime.Now} , Thread {Thread.CurrentThread.ManagedThreadId} - Hello World!, is thread pool: {Thread.CurrentThread.IsThreadPoolThread}");
                return ConsumeAsync(stoppingToken);
            }, stoppingToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            Console.WriteLine("Task Completed");
            return Task.CompletedTask;
        }

        Task ConsumeAsync(CancellationToken cancellationToken)
        {
            var testList = new List<int>();
            for (var i = 0; i < 100; i++)
            {
                testList.Add(i);
            }
            try
            {
                Console.WriteLine($"4 ConsumeAsync Start. Time :{DateTime.Now} , Thread {Thread.CurrentThread.ManagedThreadId} - Hello World!, is thread pool: {Thread.CurrentThread.IsThreadPoolThread}");
                return TestAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"top job due to encounter error, {ex.Message}");
                throw;
            }

        }

        Task TestAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"5 TestAsync Start Time :{DateTime.Now} , Thread {Thread.CurrentThread.ManagedThreadId} - Hello World!, is background thread: {Thread.CurrentThread.IsThreadPoolThread}");
            try
            {
                // Create the file, or overwrite if the file exists.
                using FileStream fs = File.Create(@"D:\tmp\MyTest.txt");
                byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TestAsync due to encounter error, {ex.Message}");
                throw;
            }
            return Task.CompletedTask;
        }
    }
}
