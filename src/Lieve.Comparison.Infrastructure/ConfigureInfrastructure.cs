using Lieve.Comparison.Application.Interfaces;
using Lieve.Comparison.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Lieve.Comparison.Infrastructure;

public static class ConfigureInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDb(configuration);
        
        return services;
    }

    private static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetSection("MongoSettings:ConnectionString").Value;
        var databaseName = configuration.GetSection("MongoSettings:DatabaseName").Value;

        ArgumentNullException.ThrowIfNull(connectionString, nameof(connectionString));
        ArgumentNullException.ThrowIfNull(databaseName, nameof(databaseName));

        services.AddSingleton<IMongoDbContext>(_ =>
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            return new MongoDbContext(database);
        });

        return services;
    }
}