namespace Entities;

public interface IEntity<TPrimaryKey>
{
    TPrimaryKey Id { get; set; }
}
