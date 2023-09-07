using Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {

        services.AddScoped(typeof(IBaseEntityService<>), typeof(BaseEntityService<>));
        services.AddScoped<IContentService, ContentService>();
        services.AddScoped<IContentTypeService, ContentTypeService>();
        services.AddScoped<ISiteService, SiteService>();

        return services;
    }
}
