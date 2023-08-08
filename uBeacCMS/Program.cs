using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using uBeac.Repositories.History.MongoDB;
using uBeac.Web.Logging;
using uBeac.Web.Logging.MongoDB;
using uBeacCMS;
using uBeacCMS.Middlewares;
using uBeacCMS.Models;
using uBeacCMS.Pages;
using uBeacCMS.Providers;
using uBeacCMS.Services;
using Module = uBeacCMS.Models.Module;

var builder = WebApplication.CreateBuilder(args);

// Adding json config files
builder.Configuration.AddJsonConfig(builder.Environment);

// Adding http logging
builder.Services.AddMongoDbHttpLogging<HttpLogMongoDBContext>("HttpLoggingConnection", builder.Configuration.GetInstance<MongoDbHttpLogOptions>("HttpLogging"));

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddServices();
builder.Services.AddRepositories();

// Adding CORS policy
var corsPolicyOptions = builder.Configuration.GetSection("CorsPolicy");
builder.Services.AddCorsPolicy(corsPolicyOptions);

// Adding HSTS
var hstsOptions = builder.Configuration.GetSection("Hsts");
builder.Services.AddHttpsPolicy(hstsOptions);

// Disabling automatic model state validation of ASP.NET Core
builder.Services.DisableAutomaticModelStateValidation();

// Adding debugger
builder.Services.AddDebugger();

// Adding mongodb
builder.Services.AddMongo<MongoDBContext>("DefaultConnection");

// Adding application context
builder.Services.AddApplicationContext();

// Adding history
builder.Services.AddMongo<HistoryMongoDBContext>("HistoryConnection");
builder.Services.AddHistory<MongoDBHistoryRepository>().For<Site>().For<Page>().For<ModuleDefinition>().For<Module>();

builder.Services.AddCms();

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddPageRoute("/Catchall", "module/{moduleName}/{type}/{id?}");
}).AddRazorRuntimeCompilation();


builder.Services.AddOptions<MvcRazorRuntimeCompilationOptions>().Configure<IServiceProvider>((options, serviceProvider) =>
{
    using (var scope = serviceProvider.CreateScope())
    {
        var pageService = scope.ServiceProvider.GetRequiredService<IPageService>();
        var siteService = scope.ServiceProvider.GetRequiredService<ISiteService>();
        options.FileProviders.Add(new DatabaseFileProvider(siteService, pageService));
    }
});

var app = builder.Build();

app.Services.SeedDefaultData();

app.UseHttpsRedirection();
app.UseHstsOnProduction(builder.Environment);
app.UseCorsPolicy(corsPolicyOptions);

app.UseStaticFiles();

app.UseRouting();

app.UseCmsRouting();

app.UseHttpLoggingMiddleware();

app.UseAuthorization();

app.MapRazorPages();
app.Run();
