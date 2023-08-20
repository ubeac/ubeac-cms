using Microsoft.AspNetCore.Builder;
using uBeacCMS.Web.Core;
using uBeacCMS.Web.Core.Middleware;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection AddCmsServices(this IServiceCollection services)
    {
        return services.AddScoped<RequestContext>();
    }

    public static IApplicationBuilder UseCmsContext(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<SiteMiddleware>();
        builder.UseMiddleware<ModuleDefinitionMiddleware>();
        builder.UseMiddleware<PageMiddleware>();
        return builder.UseMiddleware<ModuleMiddleware>();
    }
}