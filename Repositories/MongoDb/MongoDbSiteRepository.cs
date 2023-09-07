using Entities;
using MongoDB.Driver;

namespace Repositories.MongoDb;

public class MongoDbSiteRepository : MongoDbBaseEntityRepository<Site>, ISiteRepository
{
    public MongoDbSiteRepository(IMongoDbProvider databaseProvider) : base(databaseProvider)
    {
    }

    public override async Task<Site> Insert(Site site, CancellationToken cancellationToken = default)
    {
        if (site.Id ==  Guid.Empty) 
            return await base.Insert(site, cancellationToken);

        await Collection.InsertOneAsync(site, cancellationToken: cancellationToken);

        return site;
    }

    public async Task<Site> GetByDomain(string domainName, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Site>.Filter.Eq(x => x.Domain, domainName.ToLower());

        var result = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

        return result.Single(cancellationToken: cancellationToken);
    }
}
