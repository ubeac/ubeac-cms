using Entities;
using MongoDB.Driver;

namespace Repositories.MongoDb;

public class MongoDbSiteRepository : MongoDbBaseEntityRepository<Site>, ISiteRepository
{
    public MongoDbSiteRepository(IMongoDbProvider databaseProvider) : base(databaseProvider)
    {
    }

    public async Task<Site> GetByDomain(string domainName, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Site>.Filter.Eq(x => x.Domain.ToLower(), domainName.ToLower());

        var result = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

        return result.Single(cancellationToken: cancellationToken);
    }
}
