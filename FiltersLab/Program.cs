using FiltersLab.Filters.ActionFilter;
using FiltersLab.Filters.AuthorizationFilter;
using FiltersLab.Filters.ExceptionFilter;
using FiltersLab.Filters.ResourceFilter;
using FiltersLab.Filters.ResultFilter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvcCore(config =>
{
    //config.Filters.Add<HttpResponseExceptionFilter>();
    config.Filters.Add<ActionFilter>();
    config.Filters.Add<AuthorizationFilter>();
    config.Filters.Add<ExceptionFilter>();
    config.Filters.Add<ResourceFilter>();
    config.Filters.Add<ResultFilter>();

}).AddApiExplorer();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
