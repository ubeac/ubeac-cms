using Entities;

namespace Repositories.MongoDb;

public class MongoDbUserTokenRepository<TUserToken> : MongoDbBaseEntityRepository<TUserToken>, IUserTokenRepository<TUserToken> where TUserToken : UserToken
{
    public MongoDbUserTokenRepository(IMongoDbProvider mongoDbProvider) : base(mongoDbProvider)
    {
    }
}
