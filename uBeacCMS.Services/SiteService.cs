using uBeacCMS.Models;
using uBeacCMS.Repositories;

namespace uBeacCMS.Services;

public interface ISiteService : IBaseService<Site>
{
    Task<Site?> GetByDomain(string domain, CancellationToken cancellationToken = default);
}

public class SiteService : BaseService<Site>, ISiteService
{
    public SiteService(IBaseRepository<Site> repository) : base(repository)
    {
    }

    public async Task<Site?> GetByDomain(string domain, CancellationToken cancellationToken = default)
    {
        var sites = await BaseRepository.GetAll(cancellationToken);

        return sites.Where(x => x.Domains.Split(";").Contains(domain)).FirstOrDefault();

    }
}