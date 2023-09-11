using Controllers;
using Entities;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddMongoDbRepositories(builder.Configuration);

services.AddServices();

builder.Services.AddHttpContextAccessor();

services.AddScoped<IApplicationContext>(provider =>
{
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
    return new ApplicationContext(httpContextAccessor);
});

services.AddControllers().AddCmsControllers();

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

app.Services.SeedDefaultData();

app.UseHttpsRedirection();

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseAuthorization();

app.UseMiddleware<CoreMiddleware>();

app.MapControllers();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "My API V1");
});


app.Run();
