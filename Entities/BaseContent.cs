namespace Entities;

public interface IBaseContent : IBaseEntity
{
    Guid SiteId { get; set; }
    Guid TypeId { get; set; }
}

public class BaseContent : BaseEntity, IBaseContent
{
    public Guid SiteId { get; set; }
    public Guid TypeId { get; set; }
}