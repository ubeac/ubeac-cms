using Repositories.MongoDb;
using Repositories;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class MongoDbServiceExtensions
{
    public static IServiceCollection AddMongoDbRepositories(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<MongoDbConfiguration>(configuration.GetSection("MongoDbConfiguration") ?? throw new InvalidOperationException("MongoDbConfiguration section not found."));

        services.AddScoped(typeof(IBaseEntityRepository<>), typeof(MongoDbBaseEntityRepository<>));
        services.AddScoped(typeof(IBaseContentRepository<>), typeof(MongoDbBaseContentRepository<>));
        services.AddScoped<IContentTypeDefinitionRepository, MongoDbContentTypeDefinitionRepository>();
        services.AddScoped<ISiteRepository, MongoDbSiteRepository>();

        services.AddSingleton<IMongoDbProvider, MongoDbProvider>();

        return services;
    }
}
