using uBeacCMS.Models;

namespace uBeacCMS.Repositories;

public interface IBaseRepository<T> where T : IBaseEntity
{
    Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);
    Task<T> Add(T entity, CancellationToken cancellationToken = default);
    Task<T> Update(T entity, CancellationToken cancellationToken = default);
    Task<T> Delete(Guid id, CancellationToken cancellationToken = default);
    Task<T> GetById(Guid id, CancellationToken cancellationToken = default);
}

public interface ISiteRepository : IBaseRepository<Site>
{
}

