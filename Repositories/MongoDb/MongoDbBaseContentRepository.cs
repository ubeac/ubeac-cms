using Entities;
using MongoDB.Driver;

namespace Repositories.MongoDb;

public class MongoDbBaseContentRepository<TEntity> : MongoDbBaseEntityRepository<TEntity>, IBaseContentRepository<TEntity> where TEntity : class, IBaseContent
{
    public MongoDbBaseContentRepository(IMongoDbProvider databaseProvider) : base(databaseProvider)
    {
    }

    public async Task<List<TEntity>> GetAll(Guid siteId, Guid typeId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<TEntity>.Filter.Eq(x => x.SiteId, siteId);
        filter &= Builders<TEntity>.Filter.Eq(x => x.TypeId, typeId);

        var result = await Collection.FindAsync(filter , cancellationToken: cancellationToken);

        return result.ToList(cancellationToken: cancellationToken);
    }
}