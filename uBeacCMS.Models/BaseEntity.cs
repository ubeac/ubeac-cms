namespace uBeacCMS.Models;

public interface IBaseEntity
{
    DateTime CreateDate { get; set; }
    Guid CreatedBy { get; set; }
    Guid Id { get; set; }
    Guid? LastUpdateBy { get; set; }
    DateTime? LastUpdateDate { get; set; }
}

public class BaseEntity : IBaseEntity
{
    public Guid Id { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? LastUpdateBy { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? LastUpdateDate { get; set; }
}