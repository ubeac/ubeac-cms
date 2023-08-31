using Entities;
using Repositories;

namespace Services;

public class Service<TEntity> : Service<TEntity, Guid>, IService<TEntity> where TEntity : class, IEntity<Guid>
{
    public Service(IRepository<TEntity> repository) : base(repository)
    {
    }
}

public class Service<TEntity, TPrimaryKey> : IService<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
{
    protected IRepository<TEntity, TPrimaryKey> Repository { get; }

    public Service(IRepository<TEntity, TPrimaryKey> repository)
    {
        Repository = repository;
    }

    public Task Delete(TPrimaryKey id, CancellationToken cancellationToken = default)
    {
        return Repository.Delete(id, cancellationToken);
    }

    public Task<List<TEntity>> GetAll(CancellationToken cancellationToken = default)
    {
        return Repository.GetAll(cancellationToken);
    }

    public Task<TEntity> GetById(TPrimaryKey id, CancellationToken cancellationToken = default)
    {
        return Repository.GetById(id, cancellationToken);
    }

    public Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken = default)
    {
        return Repository.Insert(entity, cancellationToken);
    }

    public Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        return Repository.Update(entity, cancellationToken);
    }
}
