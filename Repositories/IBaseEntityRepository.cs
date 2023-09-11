using Entities;

namespace Repositories;

public interface IRepository
{
}

public interface IBaseEntityRepository<TEntity> : IRepository where TEntity : class, IBaseEntity
{
    IQueryable<TEntity> AsQueryable();
    Task<IList<TEntity>> GetAll(CancellationToken cancellationToken = default);
    Task<TEntity?> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> Delete(Guid id, CancellationToken cancellationToken = default);
}