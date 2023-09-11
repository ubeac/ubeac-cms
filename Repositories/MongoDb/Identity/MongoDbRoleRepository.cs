using Entities;
using MongoDB.Driver;

namespace Repositories.MongoDb;

public class MongoDbRoleRepository<TRole> : MongoDbBaseEntityRepository<TRole>, IRoleRepository<TRole> where TRole : Role
{
    public MongoDbRoleRepository(IMongoDbProvider mongoDbProvider) : base(mongoDbProvider)
    {
        // Create Indexes
        try
        {
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexKeys = Builders<TRole>.IndexKeys.Ascending(role => role.NormalizedName);
            var indexModel = new CreateIndexModel<TRole>(indexKeys, indexOptions);
            Collection.Indexes.CreateOne(indexModel);
        }
        catch (Exception)
        {
            // ignored
        }
    }

    protected override string CollectionName => "Role";

    public async Task<TRole?> FindByName(string normalizedRoleName, CancellationToken cancellationToken)
    {
        var filter = Builders<TRole>.Filter.Eq(x => x.NormalizedName, normalizedRoleName);

        var result = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

        return result.SingleOrDefault(cancellationToken: cancellationToken);
    }

}