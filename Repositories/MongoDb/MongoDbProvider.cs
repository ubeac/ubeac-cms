using Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Repositories.MongoDb;

public class MongoDbProvider : IMongoDbProvider
{
    private readonly IMongoDatabase _database;
    private readonly CmsContext _cmsContext;
    private readonly MongoClient _mongoClient;

    public MongoDbProvider(MongoClient mongoClient, CmsContext cmsContext, IOptions<MongoDbConfiguration> options)
    {
        _cmsContext = cmsContext;
        _mongoClient = mongoClient;
        _database = _mongoClient.GetDatabase($"{options.Value.DatabasePrefix}{cmsContext.SiteId}");
    }

    public IMongoDatabase Database => _database;
}