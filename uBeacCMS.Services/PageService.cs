using uBeacCMS.Models;
using uBeacCMS.Repositories;

namespace uBeacCMS.Services;

public interface IPageService : IBaseService<Page>
{
    Task<Page?> GetByRoute(Guid siteId, string route, CancellationToken cancellationToken = default);
}

public class PageService : BaseService<Page>, IPageService
{
    public PageService(IBaseRepository<Page> repository) : base(repository)
    {

    }

    public async Task<Page?> GetByRoute(Guid siteId, string route, CancellationToken cancellationToken = default)
    {
        var pages = await BaseRepository.GetAll(cancellationToken).ConfigureAwait(false);
        return pages.Where(x => x.SiteId == siteId && x.Route == route).FirstOrDefault();
    }
}
