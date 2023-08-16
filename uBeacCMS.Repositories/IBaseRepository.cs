using uBeacCMS.Models;

namespace uBeacCMS.Repositories;

public interface IBaseRepository<T> where T : IBaseEntity
{
    Task<IList<T>> GetAll(CancellationToken cancellationToken = default);
    Task Add(T entity, CancellationToken cancellationToken = default);
    Task AddRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task Update(T entity, CancellationToken cancellationToken = default);
    Task Delete(Guid id, CancellationToken cancellationToken = default);
    Task<T> GetById(Guid id, CancellationToken cancellationToken = default);
}
