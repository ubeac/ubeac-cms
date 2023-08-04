using uBeacCMS.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ISiteService, SiteService>();
        services.AddScoped<IPageService, PageService>();
        services.AddScoped<IModuleDefinitionService, ModuleDefinitionService>();
        services.AddScoped<IModuleService, ModuleService>();
        services.AddScoped<ITextHtmlService, TextHtmlService>();
        services.AddScoped<IImageService, ImageService>();
        return services;
    }
}
