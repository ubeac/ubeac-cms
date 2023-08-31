using Microsoft.Extensions.Configuration;
using Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {

        services.AddScoped(typeof(IService<>), typeof(Service<>));
        services.AddScoped(typeof(IService<,>), typeof(Service<,>));

        return services;
    }
}
