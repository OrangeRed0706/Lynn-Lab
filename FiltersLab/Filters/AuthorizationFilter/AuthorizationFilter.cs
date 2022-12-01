using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersLab.Filters.AuthorizationFilter
{
    public class AuthorizationFilter : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            Console.WriteLine("AuthorizationFilter in");
            await Task.Delay(500);
            //await context.HttpContext.Response.WriteAsync($"{GetType().Name} in. \r\n");
        }
    }
}
