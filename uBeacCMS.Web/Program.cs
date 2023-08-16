using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Configuration;
using uBeacCMS.Repositories;
using uBeacCMS.Repositories.StaticFile;
using uBeacCMS.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddServerComponents();

builder.Services.Configure<StaticFileRepositorySettings>(builder.Configuration.GetSection("FileRepository"));
builder.Services.AddOptions<StaticFileRepositorySettings>().Configure(options =>
{

});

builder.Services.AddSingleton(typeof(IBaseRepository<>), typeof(StaticFileBaseRepository<>));

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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.MapRazorComponents<App>();

app.Run();
