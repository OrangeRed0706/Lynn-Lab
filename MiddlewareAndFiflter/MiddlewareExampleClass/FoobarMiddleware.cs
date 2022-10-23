using System.Diagnostics;

namespace MiddlewareAndFiflter.MiddlewareExampleClass
{
    public class FoobarMiddleware : IMiddleware
    {
        public FoobarMiddleware(Foo foo, Bar bar)
        {
            Debug.Assert(foo != null);
            Debug.Assert(bar != null);
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Debug.Assert(next != null);
            return next(context);
        }
    }

    public class Foo { }
    public class Bar { }
}
