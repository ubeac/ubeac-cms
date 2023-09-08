using Entities;

namespace Repositories;

public interface IContentRepository
{
    Task<List<Content>> GetAll(Guid typeId, Guid siteId, CancellationToken cancellationToken = default);
    Task<Content?> GetById(Guid typeId, Guid siteId, Guid id, CancellationToken cancellationToken = default);
    Task<Content> Insert(Guid typeId, Guid siteId, Content entity, CancellationToken cancellationToken = default);
    Task<Content> Update(Guid typeId, Guid siteId, Content entity, CancellationToken cancellationToken = default);
    Task Delete(Guid typeId, Guid siteId, Guid id, CancellationToken cancellationToken = default);
}