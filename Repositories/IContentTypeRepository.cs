using Entities;

namespace Repositories;

public interface IContentTypeRepository : IBaseEntityRepository<ContentType>
{
    Task<ContentType> GetByName(string name, CancellationToken cancellationToken = default);
}
