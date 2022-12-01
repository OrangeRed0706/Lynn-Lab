using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersLab.Filters.ExceptionFilter
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            Console.WriteLine("ExceptionFilter in");
            await Task.Delay(500);
            //await context.HttpContext.Response.WriteAsync($"{GetType().Name} in. \r\n");
        }
    }
}
