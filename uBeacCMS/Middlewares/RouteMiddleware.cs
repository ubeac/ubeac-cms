﻿using uBeacCMS.Models;
using uBeacCMS.Services;

namespace uBeacCMS.Middlewares;

public class RouteMiddleware
{
    private readonly RequestDelegate _next;

    public RouteMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ISiteService siteService, IPageService pageService, CmsContext cmsContext, IModuleService moduleService)
    {
        var site = await siteService.GetByDomain(context.Request.Host.Value);

        if (site != null)
        {
            cmsContext.Site = site;

            var page = await pageService.GetByUrl(context.Request.Path);

            if (page != null)
            {
                var modules = await moduleService.GetByPageId(page.Id);

                cmsContext.Page = page;
                cmsContext.Modules = modules.ToList();
            }
        }

        await _next(context);
    }
}

public static class CoreMiddlewareExtensions
{
    public static IServiceCollection AddCms(this IServiceCollection services)
    {
        services.AddScoped(serviceProvider =>
        {
            return new CmsContext();
        });

        return services;
    }

    public static IApplicationBuilder UseCmsRouting(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RouteMiddleware>();
    }
}