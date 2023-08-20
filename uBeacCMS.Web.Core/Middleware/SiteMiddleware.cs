using Microsoft.AspNetCore.Http;
using uBeacCMS.Services;

namespace uBeacCMS.Web.Core.Middleware;

public class SiteMiddleware
{
    private readonly RequestDelegate _next;

    public SiteMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, ISiteService siteService, RequestContext context)
    {
        var site = await siteService.GetByDomain(httpContext.Request.Host.Value);
        context.Site = site;
        
        await _next(httpContext);
    }

}
