using Microsoft.AspNetCore.Http;
using uBeacCMS.Services;

namespace uBeacCMS.Web.Core.Middleware;

public class ModuleDefinitionMiddleware
{
    private readonly RequestDelegate _next;

    public ModuleDefinitionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, RequestContext context, IModuleDefinitionService moduleDefinitionService)
    {
        context.ModuleDefinitions = (await moduleDefinitionService.GetAll()).ToList();

        await _next(httpContext);
    }

}
