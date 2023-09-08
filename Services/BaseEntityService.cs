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
    protected CmsContext Context { get; }

    public BaseEntityService(IBaseEntityRepository<TEntity> repository, CmsContext cmsContext)
    {
        Repository = repository;
        Context = cmsContext;
    }

    public virtual Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        return Repository.Delete(id, cancellationToken);
    }

    public virtual Task<List<TEntity>> GetAll(CancellationToken cancellationToken = default)
    {
        return Repository.GetAll(cancellationToken);
    }

    public virtual Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return Repository.GetById(id, cancellationToken);
    }

    public virtual Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.CreateDate = DateTime.Now;
        entity.CreateBy = Context.Username;
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
        entity.LastUpdateBy = Context.Username;
        return Repository.Update(entity, cancellationToken);
    }
}
