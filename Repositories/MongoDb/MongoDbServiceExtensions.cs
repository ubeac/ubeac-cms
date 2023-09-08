using Repositories.MongoDb;
using Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class MongoDbServiceExtensions
{
    public static IServiceCollection AddMongoDbRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        // For Guid serialization 
        BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(GuidRepresentation.Standard));

        // This is important: if we remove this line, filtering by Guid properties won't work
        BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;

        services.Configure<MongoDbConfiguration>(configuration.GetSection("MongoDbConfiguration") ?? throw new InvalidOperationException("MongoDbConfiguration section not found."));

        services.AddScoped(typeof(IBaseEntityRepository<>), typeof(MongoDbBaseEntityRepository<>));
        services.AddScoped<IContentRepository, MongoDbContentRepository>();
        services.AddScoped<IContentTypeRepository, MongoDbContentTypeRepository>();
        services.AddScoped<ISiteRepository, MongoDbSiteRepository>();

        services.AddScoped<IMongoDbProvider, MongoDbProvider>();

        services.AddSingleton(provider =>
        {
            var options = provider.GetRequiredService<IOptions<MongoDbConfiguration>>();
            return new MongoClient(options.Value.ConnectionString);
        });

        return services;
    }
}
