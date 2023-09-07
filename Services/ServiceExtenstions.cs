using Entities;
using Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {

        services.AddScoped(typeof(IBaseEntityService<>), typeof(BaseEntityService<>));
        services.AddScoped(typeof(IBaseContentService<>), typeof(BaseContentService<>));
        services.AddScoped<ISiteService, SiteService>();     
        services.AddScoped(serviceProvider =>
        {
            return new CmsContext();
        });

        return services;
    }
}
