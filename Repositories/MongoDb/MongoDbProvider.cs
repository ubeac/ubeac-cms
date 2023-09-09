using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Repositories.MongoDb;

public interface IMongoDbProvider
{
    IMongoDatabase Database { get; }
}

public class MongoDbProvider : IMongoDbProvider
{
    private readonly IMongoDatabase _database;
    private readonly MongoClient _mongoClient;

    public MongoDbProvider(MongoClient mongoClient, IOptions<MongoDbConfiguration> options)
    {
        _mongoClient = mongoClient;
        _database = _mongoClient.GetDatabase(options.Value.Database);
    }

    public IMongoDatabase Database => _database;
}