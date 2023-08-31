using Repositories.MongoDb;
using Repositories;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class MongoDbServiceExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<MongoDbConfiguration>(configuration.GetSection("MongoDbConfiguration") ?? throw new InvalidOperationException("MongoDbConfiguration section not found."));

        services.AddScoped(typeof(IRepository<>), typeof(MongoDbRepositoryBase<>));
        services.AddScoped(typeof(IRepository<,>), typeof(MongoDbRepositoryBase<,>));

        services.AddSingleton<IMongoDbProvider, MongoDbProvider>();

        return services;
    }
}
