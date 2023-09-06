using Entities;

namespace Repositories;

public interface ISiteRepository : IBaseEntityRepository<Site>
{
    Task<Site> GetByDomain(string domainName, CancellationToken cancellationToken = default);
}
