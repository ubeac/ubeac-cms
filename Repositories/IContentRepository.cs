using Entities;

namespace Repositories;

public interface IContentRepository
{
    Task<List<Content>> GetAll(string type, Guid siteId, CancellationToken cancellationToken = default);
    Task<Content?> GetById(string type, Guid id, CancellationToken cancellationToken = default);
    Task<Content> Insert(string type, Content entity, CancellationToken cancellationToken = default);
    Task<Content> Update(string type, Content entity, CancellationToken cancellationToken = default);
    Task Delete(string type, Guid id, CancellationToken cancellationToken = default);
}