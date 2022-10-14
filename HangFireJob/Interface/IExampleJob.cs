using Hangfire;

namespace HangFireJob.Interface;

public interface IExampleJob
{
    [Queue("critical")]
    [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
    Task Run();
}