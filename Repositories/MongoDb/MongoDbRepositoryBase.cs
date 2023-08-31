using Entities;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Repositories.MongoDb;

public class MongoDbRepositoryBase<TEntity> : MongoDbRepositoryBase<TEntity, Guid>, IRepository<TEntity> where TEntity : class, IEntity<Guid>
{
    public MongoDbRepositoryBase(IMongoDbProvider databaseProvider) : base(databaseProvider)
    {
    }

    public override Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.Id = Guid.NewGuid();
        return base.Insert(entity, cancellationToken);
    }
}

public class MongoDbRepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
{

    private readonly IMongoDbProvider _databaseProvider;

    protected virtual IMongoDatabase Database => _databaseProvider.Database;
    protected virtual string CollectionName => typeof(TEntity).Name;
    protected virtual IMongoCollection<TEntity> Collection => _databaseProvider.Database.GetCollection<TEntity>(CollectionName);


    public MongoDbRepositoryBase(IMongoDbProvider databaseProvider)
    {
        _databaseProvider = databaseProvider;
    }

    protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
    {
        var lambdaParam = Expression.Parameter(typeof(TEntity));

        var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");

        if (id == null) throw new ArgumentException($"Id is null.");

        var idValue = Convert.ChangeType(id, typeof(TPrimaryKey)) ?? throw new ArgumentException($"Unable to convert type {id.GetType()} to {typeof(TPrimaryKey)}");

        Expression<Func<object>> closure = () => idValue;
        var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);

        var lambdaBody = Expression.Equal(leftExpression, rightExpression);

        return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
    }

    public IQueryable<TEntity> AsQueryable()
    {
        return Collection.AsQueryable();
    }

    public async Task<List<TEntity>> GetAll(CancellationToken cancellationToken = default)
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

    public virtual async Task<TEntity> GetById(TPrimaryKey id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);

        var result = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

        return result.SingleOrDefault(cancellationToken: cancellationToken);
    }

    public virtual async Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken = default)
    {
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

    public virtual async Task Delete(TPrimaryKey id, CancellationToken cancellationToken = default)
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