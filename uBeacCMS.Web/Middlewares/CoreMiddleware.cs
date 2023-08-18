﻿using uBeacCMS.Models;
using uBeacCMS.Services;

namespace uBeacCMS.Web.Middlewares;

public class Context
{
    public Page? Page { get; set; }
    public Site? Site { get; set; }
    public List<Module>? Modules { get; set; }

}

public class RouteMiddleware
{
    private readonly RequestDelegate _next;

    public RouteMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, ISiteService siteService, IPageService pageService, Context context, IModuleDefinitionService moduleDefinitionService, IModuleService moduleService)
    {
        var site = await siteService.GetByDomain(httpContext.Request.Host.Value);
        context.Site = site;
        if (site != null)
        {
            var page = await pageService.GetByRoute(site.Id, httpContext.Request.Path.Value.ToLower());
            context.Page = page;
            if (page != null) 
            {
                var modules = await moduleService.GetByPageId(page.Id);
                context.Modules = modules.ToList();
            }            
        }

        await _next(httpContext);
    }
}

public static class CoreMiddlewareExtensions
{
    public static IApplicationBuilder UseCmsContext(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RouteMiddleware>();
    }
}