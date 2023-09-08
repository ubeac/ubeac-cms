using Entities;
using MongoDB.Driver;

namespace Repositories.MongoDb;

public class MongoDbContentTypeRepository : MongoDbBaseEntityRepository<ContentType>, IContentTypeRepository
{
    public MongoDbContentTypeRepository(IMongoDbProvider databaseProvider) : base(databaseProvider)
    {

    }

    public async Task<ContentType> GetByName(Guid siteId, string name, CancellationToken cancellationToken = default)
    {
        var filter = Builders<ContentType>.Filter.Eq(x => x.Name, name);
        filter &= Builders<ContentType>.Filter.Eq(x => x.SiteId, siteId);

        var result = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

        return result.Single(cancellationToken: cancellationToken);
    }
}
