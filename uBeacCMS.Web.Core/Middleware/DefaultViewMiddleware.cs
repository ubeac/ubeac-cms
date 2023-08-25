using Microsoft.AspNetCore.Http;
using uBeacCMS.Models;
using uBeacCMS.Services;

namespace uBeacCMS.Web.Core.Middleware;

public class DefaultViewMiddleware
{
    private readonly RequestDelegate _next;

    public DefaultViewMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, ViewContext context, ISiteService siteService, ISkinService skinService, IPageService pageService, IModuleDefinitionService moduleDefinitionService, IModuleService moduleService)
    {
        if (IsDynamicRoute(httpContext))
        {
            context.ModuleDefinitions = (await moduleDefinitionService.GetAll()).ToList();

            var site = await siteService.GetByDomain(httpContext.Request.Host.Value);
            if (site != null)
            {
                context.Site = site;
                context.SiteSkins = (await skinService.GetByIds(site.SkinIds)).ToList();
                context.Skin = context.SiteSkins?.Where(x => x.Type == ViewType.Normal && x.ContainerType == SkinContainerType.Site).Single();

                var page = await pageService.GetByRoute(context.Site.Id, httpContext.Request.Path.Value.ToLower());
                if (page != null)
                {
                    context.Page = page;

                    var modules = await moduleService.GetByPageId(context.Page.Id);
                    context.Modules = modules.ToList();

                    if (page.SkinId.HasValue)
                        context.Skin = await skinService.GetById(page.SkinId.Value);

                    context.ViewType = ViewType.Normal;
                }
            }

        }

        await _next(httpContext);
    }

    private bool IsDynamicRoute(HttpContext httpContext)
    {
        return httpContext.Request.Path.Value.IndexOf(".") < 0;
    }

}
