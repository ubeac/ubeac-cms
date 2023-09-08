using Entities;
using MongoDB.Driver;

namespace Repositories.MongoDb;

public class MongoDbContentRepository : MongoDbBaseEntityRepository<Content>, IContentRepository
{
    public MongoDbContentRepository(IMongoDbProvider databaseProvider) : base(databaseProvider)
    {
    }

    private FilterDefinition<Content> GetDefaultFilter(Guid typeId, Guid siteId)
    {
        var filter = Builders<Content>.Filter.Eq(x => x.SiteId, siteId);
        filter &= Builders<Content>.Filter.Eq(x => x.TypeId, typeId);

        return filter;
    }

    public async Task Delete(Guid typeId, Guid siteId, Guid id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Content>.Filter.Eq(x => x.Id, id) & GetDefaultFilter(typeId, siteId);

        await Collection.DeleteOneAsync(filter, cancellationToken);
    }

    public async Task<List<Content>> GetAll(Guid typeId, Guid siteId, CancellationToken cancellationToken = default)
    {
        var filter = GetDefaultFilter(typeId, siteId);

        var result = await Collection.FindAsync(filter, cancellationToken: cancellationToken);
        return await result.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<Content?> GetById(Guid typeId, Guid siteId, Guid id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Content>.Filter.Eq(x => x.Id, id) & GetDefaultFilter(typeId, siteId);

        var result = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

        return result.SingleOrDefault(cancellationToken: cancellationToken);
    }

    public async Task<Content> Insert(Guid typeId, Guid siteId, Content entity, CancellationToken cancellationToken = default)
    {
        entity.Id = Guid.NewGuid();
    
        await Collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        return entity;
    }

    public async Task<Content> Update(Guid typeId, Guid siteId, Content entity, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Content>.Filter.Eq(x => x.Id, entity.Id) & GetDefaultFilter(typeId, siteId);

        var options = new ReplaceOptions
        {
            IsUpsert = false
        };

        var result = await Collection.ReplaceOneAsync(filter, entity, options, cancellationToken);

        return entity;
    }
}