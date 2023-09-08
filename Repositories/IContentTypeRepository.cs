using Entities;

namespace Repositories;

public interface IContentTypeRepository : IBaseEntityRepository<ContentType>
{
    Task<ContentType> GetByName(Guid siteId, string name, CancellationToken cancellationToken = default);
}
