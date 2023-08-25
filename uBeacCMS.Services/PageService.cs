using uBeacCMS.Models;
using uBeacCMS.Repositories;

namespace uBeacCMS.Services;

public interface IPageService : IBaseService<Page>
{
    Task<Page?> GetByRoute(Guid siteId, string route, CancellationToken cancellationToken = default);

    Task<IList<Page>> GetBySiteId(Guid siteId, CancellationToken cancellationToken = default);
}

public class PageService : BaseService<Page>, IPageService
{
    public PageService(IBaseRepository<Page> repository) : base(repository)
    {

    }

    public async Task<Page?> GetByRoute(Guid siteId, string route, CancellationToken cancellationToken = default)
    {
        var segments = route.Split('/', StringSplitOptions.RemoveEmptyEntries);

        var pages = (await GetBySiteId(siteId, cancellationToken).ConfigureAwait(false)).ToList();

        Page? page = pages.Where(x => x.Route == "").FirstOrDefault();
        pages = pages.Where(x => x.SiteId == siteId).ToList();

        foreach (var segment in segments)
        {
            page = pages.Where(x => segment == x.Route).FirstOrDefault();
            if (page == null)
                return default;

            pages = pages.Where(x => x.ParentId == page.Id).ToList();
        }

        return page;

    }

    public async Task<IList<Page>> GetBySiteId(Guid siteId, CancellationToken cancellationToken = default)
    {
        var pages = await BaseRepository.GetAll(cancellationToken).ConfigureAwait(false);
        return pages.Where(x => x.SiteId == siteId).ToList();
    }
}
