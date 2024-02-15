using Lieve.Comparison.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Lieve.Comparison.Infrastructure;

public static class ConfigureInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoCollection<Airport>(configuration, "airports");
        
        return services;
    }

    private static void AddMongoCollection<T>(this IServiceCollection services, IConfiguration configuration, string collectionName)
    {
        var connectionString = configuration.GetSection("MongoSettings:ConnectionString").Value;
        var databaseName = configuration.GetSection("MongoSettings:DatabaseName").Value;

        ArgumentNullException.ThrowIfNull(connectionString, nameof(connectionString));
        ArgumentNullException.ThrowIfNull(databaseName, nameof(databaseName));
        
        var mongoClient = new MongoClient(connectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseName);

        services.AddSingleton<IMongoCollection<T>>(_ => mongoDatabase.GetCollection<T>(collectionName));
    }
}