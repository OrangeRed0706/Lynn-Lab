using ControllerLifeCycle.Extensions;
using ControllerLifeCycle.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 透過AddControllersAsServices()的Controller預設是Transient，但是可以客製改成Singleton
builder.Services.AddControllersWithViews().AddCustomerControllersAsServices();
builder.Services.AddSingleton<HelloWorld>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


