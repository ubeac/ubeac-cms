using Microsoft.Extensions.Options;
using MongoDB.Driver;
using uBeacCMS.Models;

namespace uBeacCMS.Repositories.MongoDB;

public class MongoDbSettings
{
    public string ConnectionString;
    public string Database;
}

public class BaseMongoDBRepository<T> : IBaseRepository<T> where T : IBaseEntity
{

    protected readonly IMongoCollection<T> Collection;
    private readonly MongoDbSettings settings;

    public BaseMongoDBRepository(IOptions<MongoDbSettings> options)
    {
        settings = options.Value;
        var client = new MongoClient(settings.ConnectionString);
        var db = client.GetDatabase(settings.Database);
        Collection = db.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
    }

    public virtual async Task<T> Add(T entity, CancellationToken cancellationToken = default)
    {
        var options = new InsertOneOptions { BypassDocumentValidation = false };
        await Collection.InsertOneAsync(entity, options, cancellationToken);
        return entity;
    }

    public virtual async Task<T> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        return await Collection.FindOneAndDeleteAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default)
    {
        return await Collection.Find(Builders<T>.Filter.Empty).ToListAsync(cancellationToken);
    }

    public virtual Task<T> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return Collection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<T> Update(T entity, CancellationToken cancellationToken = default)
    {
        return await Collection.FindOneAndReplaceAsync(x => x.Id == entity.Id, entity, cancellationToken: cancellationToken);
    }
}

public class SiteRepository : BaseMongoDBRepository<Site>, ISiteRepository
{
    public SiteRepository(IOptions<MongoDbSettings> options) : base(options)
    {
    }
}
