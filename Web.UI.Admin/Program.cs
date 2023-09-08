using Web.UI.Admin.ApiClients;
using Web.UI.Admin.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


builder.Services.AddHttpClient();
builder.Services.AddSingleton<WeatherForecastService>();


builder.Services.AddSingleton(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    return new ContentTypeClient("https://localhost:7101", httpClientFactory.CreateClient());
});

builder.Services.AddSingleton(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    return new ContentClient("https://localhost:7101", httpClientFactory.CreateClient());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
