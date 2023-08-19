using Microsoft.AspNetCore.Builder;
using uBeacCMS.Web.Core.Middlewares;

namespace Microsoft.Extensions.DependencyInjection;

public static class CoreMiddlewareExtensions
{
    public static IApplicationBuilder UseCmsContext(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RouteMiddleware>();
    }
}