using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersLab.Filters.ActionFilter
{
    public class ActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //await context.HttpContext.Response.WriteAsync($"{GetType().Name} in. \r\n");
            Console.WriteLine("ActionFilter in");
            await Task.Delay(500);
            //throw new Exception("ActionFilter Exception");
            await next();
            Console.WriteLine("ActionFilter Out");
            await Task.Delay(500);
            //await context.HttpContext.Response.WriteAsync($"{GetType().Name} out. \r\n");
        }
    }
}
