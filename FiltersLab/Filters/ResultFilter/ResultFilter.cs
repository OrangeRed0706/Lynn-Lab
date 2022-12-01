using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersLab.Filters.ResultFilter
{
    public class ResultFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            //await context.HttpContext.Response.WriteAsync($"{GetType().Name} in. \r\n");
            Console.WriteLine("ResultFilter in");
            await Task.Delay(500);
            await next();
            Console.WriteLine("ResultFilter Out");
            await Task.Delay(500);
            //await context.HttpContext.Response.WriteAsync($"{GetType().Name} out. \r\n");
        }
    }
}
