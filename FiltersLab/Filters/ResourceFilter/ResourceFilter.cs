using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersLab.Filters.ResourceFilter
{
    public class ResourceFilter : IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            //await context.HttpContext.Response.WriteAsync($"{GetType().Name} in. \r\n");
            Console.WriteLine("ResourceFilter in");
            await Task.Delay(500);
            await next();
            Console.WriteLine("ResourceFilter Out");
            await Task.Delay(500);
            //await context.HttpContext.Response.WriteAsync($"{GetType().Name} out. \r\n");
        }
    }
}
