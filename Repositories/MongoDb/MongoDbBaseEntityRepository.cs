using Entities;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Repositories.MongoDb;


public class MongoDbBaseEntityRepository<TEntity> : IBaseEntityRepository<TEntity> where TEntity : class, IBaseEntity
{

    private readonly IMongoDbProvider _databaseProvider;

    protected virtual IMongoDatabase Database => _databaseProvider.Database;
    protected virtual string CollectionName => typeof(TEntity).Name;
    protected virtual IMongoCollection<TEntity> Collection => _databaseProvider.Database.GetCollection<TEntity>(CollectionName);


    public MongoDbBaseEntityRepository(IMongoDbProvider databaseProvider)
    {
        _databaseProvider = databaseProvider;
    }

    public IQueryable<TEntity> AsQueryable()
    {
        return Collection.AsQueryable();
    }

    public virtual async Task<List<TEntity>> GetAll(CancellationToken cancellationToken = default)
    {
        var filter = Builders<TEntity>.Filter.Empty;
        var result = await Collection.FindAsync(filter, cancellationToken: cancellationToken);
        return await result.ToListAsync(cancellationToken: cancellationToken);
    }

    public virtual async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var result = await Collection.FindAsync(predicate, cancellationToken: cancellationToken);
        return await result.ToListAsync(cancellationToken: cancellationToken);
    }

    public virtual async Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);

        var result = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

        return result.SingleOrDefault(cancellationToken: cancellationToken);
    }

    public virtual async Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.Id = Guid.NewGuid();
        await Collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        return entity;
    }

    public virtual async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);

        var options = new ReplaceOptions
        {
            IsUpsert = false
        };

        var result = await Collection.ReplaceOneAsync(filter, entity, options, cancellationToken);

        return entity;
    }

    public virtual async Task Delete(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Delete(entity.Id, cancellationToken);
    }

    public virtual async Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);
        await Collection.DeleteOneAsync(filter, cancellationToken);
    }

    public virtual async Task Delete(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        await Collection.DeleteManyAsync(predicate, cancellationToken);
    }

    public virtual async Task<long> Count(CancellationToken cancellationToken = default)
    {
        return await Collection.CountDocumentsAsync(Builders<TEntity>.Filter.Empty, cancellationToken: cancellationToken);
    }

    public virtual async Task<long> Count(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await Collection.CountDocumentsAsync(predicate, cancellationToken: cancellationToken);
    }
}
