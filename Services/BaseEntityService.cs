using Entities;
using Repositories;

namespace Services;

public interface IBaseEntityService
{
}

public interface IBaseEntityService<TEntity> : IBaseEntityService where TEntity : class, IBaseEntity
{
    Task<List<TEntity>> GetAll(CancellationToken cancellationToken = default);
    Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default);
    Task Delete(Guid id, CancellationToken cancellationToken = default);
}


public class BaseEntityService<TEntity> : IBaseEntityService<TEntity> where TEntity : class, IBaseEntity
{
    protected IBaseEntityRepository<TEntity> Repository { get; }

    public BaseEntityService(IBaseEntityRepository<TEntity> repository)
    {
        Repository = repository;
    }

    public Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        return Repository.Delete(id, cancellationToken);
    }

    public Task<List<TEntity>> GetAll(CancellationToken cancellationToken = default)
    {
        return Repository.GetAll(cancellationToken);
    }

    public Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return Repository.GetById(id, cancellationToken);
    }

    public Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.CreateDate = DateTime.Now;
        entity.LastUpdateDate = DateTime.Now;
        return Repository.Insert(entity, cancellationToken);
    }

    public Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.LastUpdateDate = DateTime.Now;
        return Repository.Update(entity, cancellationToken);
    }
}
