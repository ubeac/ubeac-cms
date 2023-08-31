using Entities;
using System.Linq.Expressions;

namespace Services;

public interface IService 
{
}

public interface IService<TEntity> : IService<TEntity, Guid> where TEntity : class, IEntity<Guid>
{

}

public interface IService<TEntity, TPrimaryKey>: IService where TEntity : class, IEntity<TPrimaryKey>
{
    Task<List<TEntity>> GetAll(CancellationToken cancellationToken = default);
    Task<TEntity> GetById(TPrimaryKey id, CancellationToken cancellationToken = default);
    Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default);
    Task Delete(TPrimaryKey id, CancellationToken cancellationToken = default);
}
