using Entities;

namespace Repositories;

public interface IContentRepository
{
    Task<List<Content>> GetAll(Guid typeId, Guid siteId, CancellationToken cancellationToken = default);
    Task<Content?> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<Content> Insert(Content entity, CancellationToken cancellationToken = default);
    Task<Content> Update(Content entity, CancellationToken cancellationToken = default);
    Task Delete(Guid id, CancellationToken cancellationToken = default);
}