namespace Entities;

public interface IBaseContent<TPrimaryKey> : IEntity<TPrimaryKey>
{
    DateTime CreateDate { get; set; }
    TPrimaryKey CreatedBy { get; set; }
    TPrimaryKey? LastUpdateBy { get; set; }
    DateTime? LastUpdateDate { get; set; }
}
