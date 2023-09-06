namespace Entities;

public interface IBaseEntity
{
    Guid Id { get; set; }
    DateTime CreateDate { get; set; }
    string? CreateBy { get; set; }
    string? LastUpdateBy { get; set; }
    DateTime? LastUpdateDate { get; set; }
}

public class BaseEntity : IBaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreateDate { get; set; }
    public string? CreateBy { get; set; }
    public string? LastUpdateBy { get; set; }
    public DateTime? LastUpdateDate { get; set; }
}