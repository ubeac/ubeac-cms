using Entities;
using Services;

namespace Controllers;

public class BaseController<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
{
    protected IService<TEntity, TPrimaryKey> Service { get; }

    public BaseController(IService<TEntity, TPrimaryKey> service)
    {
        Service = service;
    }

    public Task Delete(TPrimaryKey id, CancellationToken cancellationToken = default)
    {
        return Service.Delete(id, cancellationToken);
    }

    public Task<List<TEntity>> GetAll(CancellationToken cancellationToken = default)
    {
        return Service.GetAll(cancellationToken);
    }

    public Task<TEntity> GetById(TPrimaryKey id, CancellationToken cancellationToken = default)
    {
        return Service.GetById(id, cancellationToken);
    }

    public Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken = default)
    {
        return Service.Insert(entity, cancellationToken);
    }

    public Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        return Service.Update(entity, cancellationToken);
    }

}
