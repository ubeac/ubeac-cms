using Entities;
using Repositories;

namespace Services;

public interface IBaseEntityService
{
}

public interface IBaseEntityService<TEntity> : IBaseEntityService where TEntity : class, IBaseEntity
{
    Task<IList<TEntity>> GetAll(CancellationToken cancellationToken = default);
    Task<TEntity?> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> Delete(Guid id, CancellationToken cancellationToken = default);
}


public class BaseEntityService<TEntity> : IBaseEntityService<TEntity> where TEntity : class, IBaseEntity
{
    protected IBaseEntityRepository<TEntity> Repository { get; }
    protected IApplicationContext Context { get; }

    public BaseEntityService(IBaseEntityRepository<TEntity> repository, IApplicationContext applicationContext)
    {
        Repository = repository;
        Context = applicationContext;
    }

    public virtual Task<bool> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        return Repository.Delete(id, cancellationToken);
    }

    public virtual Task<IList<TEntity>> GetAll(CancellationToken cancellationToken = default)
    {
        return Repository.GetAll(cancellationToken);
    }

    public virtual Task<TEntity?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return Repository.GetById(id, cancellationToken);
    }

    public virtual Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.CreateDate = DateTime.Now;
        entity.CreateBy = Context.UserId;
        entity.LastUpdateDate = default;
        entity.LastUpdateBy = default;
        return Repository.Insert(entity, cancellationToken);
    }

    public virtual Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        // todo: think of this
        //entity.CreateDate = DateTime.Now;
        //entity.CreateBy = _cmsContext.Username;
        entity.LastUpdateDate = DateTime.Now;
        entity.LastUpdateBy = Context.UserId;
        return Repository.Update(entity, cancellationToken);
    }
}
