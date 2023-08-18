namespace uBeacCMS.Models;

public interface IBaseContent : IBaseEntity
{
    Guid ModuleId { get; set; }
}

public class BaseContent : BaseEntity, IBaseContent
{
    public Guid ModuleId { get; set; }
}
