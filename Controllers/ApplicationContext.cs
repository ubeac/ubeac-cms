using Entities;
using Microsoft.AspNetCore.Http;

namespace Controllers;

public class ApplicationContext : IApplicationContext
{
    protected IHttpContextAccessor Accessor;
    protected const string SidHeaderKey = "sid";
    protected const string UidHeaderKey = "uid";

    public ApplicationContext(IHttpContextAccessor? accessor)
    {
        if (accessor != null)
        {
            Accessor = accessor;

            TraceId = Accessor.HttpContext?.TraceIdentifier;
            UniqueId = Accessor.HttpContext?.Request?.Headers?.FirstOrDefault(_ => _.Key.Equals(UidHeaderKey, StringComparison.OrdinalIgnoreCase)).Value;
            SessionId = Accessor.HttpContext?.Request?.Headers?.FirstOrDefault(_ => _.Key.Equals(SidHeaderKey, StringComparison.OrdinalIgnoreCase)).Value;
            UserName = Accessor.HttpContext?.User?.Identity?.Name;
            UserIp = Accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            Language = Accessor.HttpContext?.Request?.GetTypedHeaders().AcceptLanguage.FirstOrDefault()?.Value.Value ?? "en-US";
        }
    }

    public string TraceId { get; }
    public string UniqueId { get; } // UID
    public string SessionId { get; } // SID
    public string UserName { get; }
    public string UserIp { get; }
    public string Language { get; }

    public Guid SiteId { get; }

    public Guid? UserId { get; }

    public bool IsUserInRole(string role) => Accessor.HttpContext.User.IsInRole(role);
}