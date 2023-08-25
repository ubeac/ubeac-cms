using uBeacCMS.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
        services.AddScoped(typeof(IBaseContentService<>), typeof(BaseContentService<>));
        services.AddScoped<ISiteService, SiteService>();
        services.AddScoped<IPageService, PageService>();
        services.AddScoped<IModuleService, ModuleService>();
        services.AddScoped<ISkinService, SkinService>();
        services.AddScoped<IModuleDefinitionService, ModuleDefinitionService>();
    }
}
