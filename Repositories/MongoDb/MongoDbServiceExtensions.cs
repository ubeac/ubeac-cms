﻿using Repositories.MongoDb;
using Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Entities;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class MongoDbServiceExtensions
{
    public static IServiceCollection AddMongoDbRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        // For Guid serialization 
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        services.Configure<MongoDbConfiguration>(configuration.GetSection("MongoDbConfiguration") ?? throw new InvalidOperationException("MongoDbConfiguration section not found."));

        services.AddScoped(typeof(IBaseEntityRepository<>), typeof(MongoDbBaseEntityRepository<>));
        services.AddScoped<IContentRepository, MongoDbContentRepository>();
        services.AddScoped<IContentTypeRepository, MongoDbContentTypeRepository>();
        services.AddScoped<ISiteRepository, MongoDbSiteRepository>();

        services.AddSingleton<IMongoDbProvider, MongoDbProvider>();

        return services;
    }
}
