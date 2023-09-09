using Entities;
using MongoDB.Driver;

namespace Repositories.MongoDb;

public class MongoDbUserRepository<TUser> : MongoDbBaseEntityRepository<TUser>, IUserRepository<TUser> where TUser : User
{
    public MongoDbUserRepository(IMongoDbProvider mongoDbProvider) : base(mongoDbProvider)
    {
        // Create Indexes
        try
        {
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexKeys = Builders<TUser>.IndexKeys.Ascending(user => user.NormalizedUserName);
            var indexModel = new CreateIndexModel<TUser>(indexKeys, indexOptions);
            Collection.Indexes.CreateOne(indexModel);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}
