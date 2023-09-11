using Entities;
using MongoDB.Driver;
using System.Security.Claims;

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

    public Task<IList<TUser>> FindByClaim(Claim claim, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<TUser?> FindByEmail(string normalizedEmail, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<TUser?> FindByLogin(string loginProvider, string providerKey, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IList<TUser>> FindByRoleId(string roleId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<TUser?> FindByUserName(string normalizedUserName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
