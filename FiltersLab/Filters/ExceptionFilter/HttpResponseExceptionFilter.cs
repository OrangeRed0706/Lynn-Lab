using FiltersLab.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersLab.Filters.ExceptionFilter
{
    public class HttpResponseExceptionFilter : IAsyncActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var result = await next();
            if (result.Exception is HttpResponseException httpResponseException)
            {
                context.Result = new ObjectResult(httpResponseException.Value)
                {
                    StatusCode = httpResponseException.StatusCode
                };

                result.ExceptionHandled = true;

            }
        }
    }
}
