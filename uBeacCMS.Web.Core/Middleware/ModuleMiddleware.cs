using Microsoft.AspNetCore.Http;
using uBeacCMS.Services;

namespace uBeacCMS.Web.Core.Middleware;

public class ModuleMiddleware
{
    private readonly RequestDelegate _next;

    public ModuleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, RequestContext context, IModuleService moduleService)
    {
        if (context.Page != null)
        {
            var modules = await moduleService.GetByPageId(context.Page.Id);
            context.Modules = modules.ToList();
        }
        await _next(httpContext);
    }
}