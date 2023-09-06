using Entities;
using MongoDB.Driver;

namespace Repositories.MongoDb;

public class MongoDbContentTypeDefinitionRepository : MongoDbBaseEntityRepository<ContentTypeDefinition>, IContentTypeDefinitionRepository
{
    public MongoDbContentTypeDefinitionRepository(IMongoDbProvider databaseProvider) : base(databaseProvider)
    {

    }

    public async Task<ContentTypeDefinition> GetByName(string name, CancellationToken cancellationToken = default)
    {
        var filter = Builders<ContentTypeDefinition>.Filter.Eq(x => x.Name.ToLower(), name.ToLower());

        var result = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

        return result.Single(cancellationToken: cancellationToken);
    }
}
