using MongoDB.Driver;
using uBeac.Repositories;
using uBeac.Repositories.History;
using uBeacCMS.Models;

namespace uBeacCMS.Repositories;

public interface ISiteRepository : IEntityRepository<Site>
{
    Task<Site> GetByDomain(string domain, CancellationToken cancellationToken = default);
}
public class SiteRepository : MongoEntityRepository<Site, MongoDBContext>, ISiteRepository
{
    public SiteRepository(MongoDBContext mongoDbContext, IApplicationContext applicationContext, IHistoryManager history) : base(mongoDbContext, applicationContext, history)
    {
    }

    public async Task<Site> GetByDomain(string domain, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Site>.Filter.AnyEq(x => x.Domains, domain.ToLower());
        return (await Collection.FindAsync(filter)).SingleOrDefault();
    }
}
