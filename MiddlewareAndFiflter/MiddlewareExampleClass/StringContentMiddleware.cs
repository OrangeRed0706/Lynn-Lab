namespace MiddlewareAndFiflter.MiddlewareExampleClass;

public sealed class StringContentMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _contents;
    private readonly bool _foreWardToNext;

    public StringContentMiddleware(RequestDelegate next, string contents, bool foreWardToNext = true)
    {
        _next = next;
        _contents = contents;
        _foreWardToNext = foreWardToNext;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await context.Response.WriteAsync(_contents);
        if (_foreWardToNext)
        {
            await _next(context);
        }
    }
}