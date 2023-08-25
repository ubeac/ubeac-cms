using Microsoft.AspNetCore.Http;
using uBeacCMS.Models;
using uBeacCMS.Services;

namespace uBeacCMS.Web.Core.Middleware;

public class ModuleEditViewMiddleware
{
    private readonly RequestDelegate _next;

    public ModuleEditViewMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, ViewContext context, IModuleService moduleService)
    {
        // /module/edit/[moduleId]
        if (context.Page == null && context.Site != null)
        {
            var segments = httpContext.Request.Path.Value.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length == 3 && segments[0].ToLower() == "module" && segments[1].ToLower() == "edit")
            {
                if (Guid.TryParse(segments[2], out Guid moduleId))
                {
                    var module = await moduleService.GetById(moduleId);
                    if (module != null)
                    {
                        context.Modules = new List<Module> { module };
                        context.ViewType = ViewType.Edit;
                        context.Skin = context.SiteSkins?.Where(x => x.Type == ViewType.Edit).Single();
                    }
                }
            }
        }

        await _next(httpContext);
    }
}

