using MongoDB.Driver;

namespace Repositories.MongoDb;

public interface IMongoDbProvider
{
    IMongoDatabase Database { get; }
}
