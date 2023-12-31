using Microsoft.AspNetCore.Server.Kestrel.Core;
using uBeacCMS.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddServerComponents();

builder.Services.AddStaticFileRepositories(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddCmsServices();

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});


// If using IIS:
builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

var app = builder.Build();

app.Services.SeedDefaultData();

app.UseStaticFiles();

app.UseCmsContext();

app.MapRazorComponents<App>();

app.Run();
