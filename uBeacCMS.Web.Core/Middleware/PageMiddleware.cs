using Microsoft.AspNetCore.Http;
using uBeacCMS.Models;
using uBeacCMS.Services;

namespace uBeacCMS.Web.Core.Middleware;


public class PageMiddleware
{
    private readonly RequestDelegate _next;

    public PageMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IPageService pageService, RequestContext context)
    {
        if (context.Site != null)
        {
            context.Page = await pageService.GetByRoute(context.Site.Id, httpContext.Request.Path.Value.ToLower());
        }

        await _next(httpContext);
    }
}
