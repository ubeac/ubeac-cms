using Microsoft.AspNetCore.Http;
using uBeacCMS.Services;

namespace uBeacCMS.Web.Core.Middleware;

public class ModuleEditMiddleware
{
    private readonly RequestDelegate _next;

    public ModuleEditMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, RequestContext context, IModuleService moduleService)
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
                        context.Modules = new List<Models.Module> { module };
                        context.ViewType = RequestViewType.Edit;
                        context.IsValid = true;
                    }
                }
            }
        }

        await _next(httpContext);
    }
}

