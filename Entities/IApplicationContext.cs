namespace Entities;

public interface IApplicationContext
{
    public Guid SiteId { get; set; }
    public string TraceId { get; }
    public string UniqueId { get; }
    public string SessionId { get; }
    public string? UserName { get; }
    public Guid? UserId { get; }
    public string UserIp { get; }
    public string Language { get; }
}