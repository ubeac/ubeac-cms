using uBeacCMS.Models;
using uBeacCMS.Services;
using System.Linq;

namespace uBeacCMS.Middlewares;

public class RouteMiddleware
{
    private readonly RequestDelegate _next;

    public RouteMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ISiteService siteService, IPageService pageService, CmsContext cmsContext, IModuleService moduleService, IModuleDefinitionService moduleDefinitionService)
    {
        var site = await siteService.GetByDomain(context.Request.Host.Value);

        if (site != null)
        {
            cmsContext.Site = site;

            Page page;
            var modules = new List<Module>();

            if (context.Request.Path.HasValue && context.Request.Path.Value.ToLower().Contains("/module/"))
            {
                var pathSections = context.Request.Path.Value.ToLower().Split('/');
                if (string.IsNullOrEmpty(pathSections.First()))
                    pathSections = pathSections.Skip(1).ToArray();

                var systemPath = "";

                // http://localhost:5205/module/texthtml/setting/b1f0a3b9-a158-4ed2-abf4-153ef1cd4d5e

                if (pathSections.Length == 4) 
                {
                    var moduleId = Guid.Parse(pathSections[3]);
                    var moduleDefName = pathSections[1];
                    var moduleAction = pathSections[2];
                    cmsContext.ModuleAction = moduleAction;
                    cmsContext.ModuleDefinition = moduleDefinitionService.GetByName(moduleDefName).GetAwaiter().GetResult();
                    cmsContext.Module = moduleService.GetById(moduleId).Result;
                                        
                }
                    page = await pageService.GetByUrl("home");
            }
            else
            {
                page = await pageService.GetByUrl(context.Request.Path);
                if (page != null)                
                    modules = (await moduleService.GetByPageId(page.Id)).ToList();
            }

            if (page != null)
            {
                cmsContext.Page = page;
                cmsContext.Modules = modules;

                if (cmsContext.Modules is not null && cmsContext.Modules.Count > 0)
                {
                    var moduleDefinitionIds = cmsContext.Modules.Select(x => x.ModuleDefinitionId).Distinct();
                    cmsContext.ModuleDefinitions = (await moduleDefinitionService.GetByIds(moduleDefinitionIds)).ToList();
                }
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