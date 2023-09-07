namespace Entities;

public class Content : BaseEntity
{
    public Guid SiteId { get; set; }
    public Guid TypeId { get; set; }
    public Dictionary<string, object> Fields { get; set; } = new Dictionary<string, object>();
}