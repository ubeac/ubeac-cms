using Entities;

namespace Repositories;

public interface IBaseContentRepository<TEntity> : IBaseEntityRepository<TEntity> where TEntity : class, IBaseContent
{
    Task<List<TEntity>> GetAll(Guid siteId, Guid typeId, CancellationToken cancellationToken = default);
}