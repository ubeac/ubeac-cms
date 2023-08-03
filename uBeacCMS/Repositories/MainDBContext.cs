namespace uBeacCMS.Repositories;

public class MainDBContext : BaseMongoDBContext<MainDBContext>
{
    public MainDBContext(MongoDBOptions<MainDBContext> dbOptions, BsonSerializationOptions bsonSerializationOptions) : base(dbOptions, bsonSerializationOptions)
    {
    }
}
