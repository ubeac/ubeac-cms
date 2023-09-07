using Entities;
using MongoDB.Driver;

namespace Repositories.MongoDb;

public class MongoDbContentRepository : IContentRepository
{
    private readonly IMongoDbProvider _databaseProvider;
    private readonly CmsContext _context;

    public MongoDbContentRepository(IMongoDbProvider databaseProvider, CmsContext context)
    {
        _databaseProvider = databaseProvider;
        _context = context;
    }

    protected virtual IMongoCollection<Content> GetCollection(string type) => _databaseProvider.Database.GetCollection<Content>(type);

    public async Task Delete(string type, Guid id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Content>.Filter.Eq(x => x.Id, id);
        await GetCollection(type).DeleteOneAsync(filter, cancellationToken);
    }

    public async Task<List<Content>> GetAll(string type, Guid siteId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Content>.Filter.Empty;
        var result = await GetCollection(type).FindAsync(filter, cancellationToken: cancellationToken);
        return await result.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<Content?> GetById(string type, Guid id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Content>.Filter.Eq(x => x.Id, id);

        var result = await GetCollection(type).FindAsync(filter, cancellationToken: cancellationToken);

        return result.SingleOrDefault(cancellationToken: cancellationToken);
    }

    public async Task<Content> Insert(string type, Content entity, CancellationToken cancellationToken = default)
    {
        entity.Id = Guid.NewGuid();
        await GetCollection(type).InsertOneAsync(entity, cancellationToken: cancellationToken);
        return entity;
    }

    public async Task<Content> Update(string type, Content entity, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Content>.Filter.Eq(x => x.Id, entity.Id);

        var options = new ReplaceOptions
        {
            IsUpsert = false
        };

        var result = await GetCollection(type).ReplaceOneAsync(filter, entity, options, cancellationToken);

        return entity;
    }
}