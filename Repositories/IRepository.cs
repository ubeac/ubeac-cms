using Entities;
using System.Linq.Expressions;

namespace Repositories;

public interface IRepository
{
}

public interface IRepository<TEntity> : IRepository<TEntity, Guid> where TEntity : class, IEntity<Guid>
{

}

public interface IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : class, IEntity<TPrimaryKey>
{
    #region Select/Get/Query

    IQueryable<TEntity> AsQueryable();
    Task<List<TEntity>> GetAll(CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<TEntity> GetById(TPrimaryKey id, CancellationToken cancellationToken = default);

    #endregion

    #region Insert

    Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken = default);

    #endregion

    #region Update
    Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default);
    #endregion

    #region Delete
    Task Delete(TEntity entity, CancellationToken cancellationToken = default);
    Task Delete(TPrimaryKey id, CancellationToken cancellationToken = default);
    Task Delete(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    #endregion

    #region Aggregates
    Task<long> Count(CancellationToken cancellationToken = default);
    Task<long> Count(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    #endregion
}

