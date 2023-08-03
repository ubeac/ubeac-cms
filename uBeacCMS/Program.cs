using FluentValidation.AspNetCore;
using System.Reflection;
using uBeac.Repositories.History.MongoDB;
using uBeac.Web.Logging;
using uBeac.Web.Logging.MongoDB;
using uBeacCMS;
using uBeacCMS.Models;
using Module = uBeacCMS.Models.Module;

var builder = WebApplication.CreateBuilder(args);

// Adding json config files
builder.Configuration.AddJsonConfig(builder.Environment);

// Adding http logging
builder.Services.AddMongoDbHttpLogging<HttpLogMongoDBContext>("HttpLoggingConnection", builder.Configuration.GetInstance<MongoDbHttpLogOptions>("HttpLogging"));

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

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

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseHstsOnProduction(builder.Environment);
app.UseCorsPolicy(corsPolicyOptions);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseHttpLoggingMiddleware();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
