using Entities;
using MongoDB.Driver;

namespace Repositories.MongoDb;

public class MongoDbContentRepository : IContentRepository
{
    private readonly IMongoDbProvider _databaseProvider;

    public MongoDbContentRepository(IMongoDbProvider databaseProvider)
    {
        _databaseProvider = databaseProvider;
    }

    protected virtual IMongoCollection<Content> GetCollection() => _databaseProvider.Database.GetCollection<Content>(typeof(Content).Name);

    public async Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Content>.Filter.Eq(x => x.Id, id);
        await GetCollection().DeleteOneAsync(filter, cancellationToken);
    }

    public async Task<List<Content>> GetAll(Guid typeId, Guid siteId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Content>.Filter.Eq(x => x.SiteId, siteId);
        filter &= Builders<Content>.Filter.Eq(x => x.TypeId, typeId);

        var result = await GetCollection().FindAsync(filter, cancellationToken: cancellationToken);
        return await result.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<Content?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Content>.Filter.Eq(x => x.Id, id);

        var result = await GetCollection().FindAsync(filter, cancellationToken: cancellationToken);

        return result.SingleOrDefault(cancellationToken: cancellationToken);
    }

    public async Task<Content> Insert(Content entity, CancellationToken cancellationToken = default)
    {
        entity.Id = Guid.NewGuid();
        await GetCollection().InsertOneAsync(entity, cancellationToken: cancellationToken);
        return entity;
    }

    public async Task<Content> Update(Content entity, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Content>.Filter.Eq(x => x.Id, entity.Id);

        var options = new ReplaceOptions
        {
            IsUpsert = false
        };

        var result = await GetCollection().ReplaceOneAsync(filter, entity, options, cancellationToken);

        return entity;
    }
}