using Entities;
using MongoDB.Driver;
using System.Linq;
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

    protected override string CollectionName => "User";

    public Task<IList<TUser>> FindByClaim(Claim claim, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<TUser?> FindByEmail(string normalizedEmail, CancellationToken cancellationToken)
    {
        var filter = Builders<TUser>.Filter.Eq(x => x.NormalizedEmail, normalizedEmail);

        var result = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

        return result.SingleOrDefault(cancellationToken: cancellationToken);
    }

    public Task<TUser?> FindByLogin(string loginProvider, string providerKey, CancellationToken cancellationToken)
    {
        var result = Collection.AsQueryable().Where(user => user.Logins.Any(x => x.LoginProvider == loginProvider && x.ProviderKey == providerKey));

        return Task.FromResult(result.SingleOrDefault());
    }

    public async Task<IList<TUser>> FindByRoleId(string roleId, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(roleId, out var id))
            return new List<TUser>();

        var filter = Builders<TUser>.Filter.AnyIn(x => x.Roles, new Guid[] { id });

        var result = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

        return result.ToList(cancellationToken: cancellationToken);
    }

    public async Task<TUser?> FindByUserName(string normalizedUserName, CancellationToken cancellationToken)
    {
        var filter = Builders<TUser>.Filter.Eq(x => x.NormalizedUserName, normalizedUserName);

        var result = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

        return result.SingleOrDefault(cancellationToken: cancellationToken);
    }
}
