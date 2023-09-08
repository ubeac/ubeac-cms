using Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Repositories.MongoDb;

public class MongoDbProvider : IMongoDbProvider
{
    private readonly IMongoDatabase _database;

    public MongoDbProvider(IOptions<MongoDbConfiguration> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);

        _database = mongoClient.GetDatabase(options.Value.DatabaseName);
    }

    public IMongoDatabase Database => _database;
}