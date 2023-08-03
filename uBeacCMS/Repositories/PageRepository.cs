using MongoDB.Driver;
using uBeac.Repositories;
using uBeac.Repositories.History;
using uBeacCMS.Models;

namespace uBeacCMS.Repositories;

public interface IPageRepository : IEntityRepository<Page>
{
    Task<Page> GetByUrl(string url, CancellationToken cancellationToken = default);
    Task<IEnumerable<Page>> GetBySiteId(Guid siteId, CancellationToken cancellationToken = default);
}
public class PageRepository : MongoEntityRepository<Page, MongoDBContext>, IPageRepository
{
    public PageRepository(MongoDBContext mongoDbContext, IApplicationContext applicationContext, IHistoryManager history) : base(mongoDbContext, applicationContext, history)
    {
    }

    public async Task<IEnumerable<Page>> GetBySiteId(Guid siteId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Page>.Filter.Eq(x => x.SiteId, siteId);
        return (await Collection.FindAsync(filter)).ToEnumerable();
    }

    public async Task<Page> GetByUrl(string url, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Page>.Filter.Eq(x => x.Url, url.ToLower());
        return (await Collection.FindAsync(filter)).SingleOrDefault();
    }
}
