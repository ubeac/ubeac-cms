using Entities;
using Microsoft.AspNetCore.Http;
using Services;

namespace Controllers;

public class CoreMiddleware
{
    private readonly RequestDelegate _next;

    public CoreMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, ISiteService siteService, IApplicationContext applicationContext)
    {

        var domainName = httpContext.Request.Host.Value;
        var site = await siteService.GetByDomain(domainName);
        if (site != null)
        {
            applicationContext.SiteId = site.Id;
        }

        await _next(httpContext);
    }

}
