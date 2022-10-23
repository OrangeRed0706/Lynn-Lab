using System.Text;
using MiddlewareAndFiflter.MiddlewareExampleClass;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<FoobarMiddleware>().AddSingleton<Foo>().AddSingleton<Bar>();
builder.Services.AddHttpClient("HelloHttpClient", client => client.BaseAddress = new Uri("https://localhost:5001"));

var app = builder.Build();
app.UseMiddleware<FoobarMiddleware>();

app.UseMiddleware<StringContentMiddleware>("There are");
app.UseMiddleware<StringContentMiddleware>(" All Services \n");

app.Run(InvokeAsync);
app.Run();

Task InvokeAsync(HttpContext httpContext)
{
    var sb = new StringBuilder();
    sb.AppendLine(@$"{nameof(ConstString.ServiceLifetime),-15} {nameof(ConstString.ServiceType),-60} {nameof(ConstString.ImplementationTypeName),-30}");

    foreach (var service in builder.Services)
    {
        var serviceTypeName = GetName(service.ServiceType);
        var implementationTypeName = service.ImplementationType 
                                     ?? service.ImplementationInstance?.GetType() 
                                     ?? service.ImplementationFactory
                                     ?.Invoke(httpContext.RequestServices)?.GetType();
        if (implementationTypeName != null)
        {
            sb.AppendLine(@$"{service.Lifetime,-15} {GetName(service.ServiceType),-60} {GetName(implementationTypeName),-30}");
        }
    }
    return httpContext.Response.WriteAsync(sb.ToString());
}

static string GetName(Type type)
{
    if (!type.IsGenericType)
    {
        return type.Name;
    }
    var name = type.Name.Split('`')[0];
    var args = type.GetGenericArguments().Select(it => it.Name);
    
    return $@"{name}<{string.Join(",", args)}>";
}

static class ConstString
{
    public static string ServiceLifetime = "serviceLifetime";
    public static string ServiceType = "ServiceType";
    public static string ImplementationTypeName = "ImplementationTypeName";
}