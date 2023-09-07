using Entities;
using MongoDB.Driver;

namespace Repositories.MongoDb;

public class MongoDbContentTypeRepository : MongoDbBaseEntityRepository<ContentType>, IContentTypeRepository
{
    public MongoDbContentTypeRepository(IMongoDbProvider databaseProvider) : base(databaseProvider)
    {

    }

    public async Task<ContentType> GetByName(string name, CancellationToken cancellationToken = default)
    {
        var filter = Builders<ContentType>.Filter.Eq(x => x.Name, name);

        var result = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

        return result.Single(cancellationToken: cancellationToken);
    }
}
