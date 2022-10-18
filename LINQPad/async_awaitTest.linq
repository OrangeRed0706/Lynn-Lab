<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async Task Main()
{
	var cancellationToken = new CancellationToken();
	await Task.Run(() =>
	{
		Console.WriteLine($"1 Time :{DateTime.Now} , Thread {Thread.CurrentThread.ManagedThreadId} , is thread pool: {Thread.CurrentThread.IsThreadPoolThread}");
	}, cancellationToken);
	
	await Task.Factory.StartNew(() =>
	{
		Thread.Sleep(500);
		Console.WriteLine($"2 Time :{DateTime.Now} , Thread {Thread.CurrentThread.ManagedThreadId} , is thread pool: {Thread.CurrentThread.IsThreadPoolThread}");
		
		return Use_await(cancellationToken); //1. .
		//return Use_Task(cancellationToken); //2.
	}, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
	Console.WriteLine("Task Completed");
}
//1.
Task Use_Task(CancellationToken cancellationToken)
{
	Console.WriteLine($"3 Time :{DateTime.Now} , Thread {Thread.CurrentThread.ManagedThreadId} , is thread pool: {Thread.CurrentThread.IsThreadPoolThread}");
	Console.WriteLine("Use task");
	Task.Delay(500);
	Console.WriteLine($"4 Time :{DateTime.Now} , Thread {Thread.CurrentThread.ManagedThreadId} , is thread pool: {Thread.CurrentThread.IsThreadPoolThread}");
	return TestAsync(cancellationToken);
}
//2.
async Task Use_await(CancellationToken cancellationToken)
{
	Console.WriteLine($"3 Time :{DateTime.Now} , Thread {Thread.CurrentThread.ManagedThreadId} , is thread pool: {Thread.CurrentThread.IsThreadPoolThread}");
	Console.WriteLine("Use await");
	await Task.Delay(500);
	Console.WriteLine($"4 Time :{DateTime.Now} , Thread {Thread.CurrentThread.ManagedThreadId} , is thread pool: {Thread.CurrentThread.IsThreadPoolThread}");
	await TestAsync(cancellationToken);
}

Task TestAsync(CancellationToken cancellationToken)
{
	Console.WriteLine($"5 Time :{DateTime.Now} , Thread {Thread.CurrentThread.ManagedThreadId} , is thread pool: {Thread.CurrentThread.IsThreadPoolThread}");
	Console.WriteLine("Hello");
	
	return Task.CompletedTask;
}