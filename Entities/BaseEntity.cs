namespace Entities;

public interface IBaseEntity
{
    Guid Id { get; set; }
    DateTime CreateDate { get; set; }
    Guid? CreateBy { get; set; }
    Guid? LastUpdateBy { get; set; }
    DateTime? LastUpdateDate { get; set; }
}

public class BaseEntity : IBaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreateDate { get; set; }
    public Guid? CreateBy { get; set; }
    public Guid? LastUpdateBy { get; set; }
    public DateTime? LastUpdateDate { get; set; }
}