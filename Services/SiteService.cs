using Entities;
using Repositories;

namespace Services;

public interface ISiteService : IBaseEntityService<Site>
{
    Task<Site> GetByDomain(string domainName, CancellationToken cancellationToken = default);
}

public class SiteService : BaseEntityService<Site>, ISiteService
{

    private readonly ISiteRepository _siteRepository;
    public SiteService(ISiteRepository repository, CmsContext cmsContext) : base(repository, cmsContext)
    {
        _siteRepository = repository;
    }

    public async Task<Site> GetByDomain(string domainName, CancellationToken cancellationToken = default)
    {
        return await _siteRepository.GetByDomain(domainName, cancellationToken);
    }
}
