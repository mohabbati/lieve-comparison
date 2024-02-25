using Lieve.Comparison.Application.Interfaces;
using Lieve.Comparison.Domain.Entities;
using MongoDB.Driver;

namespace Lieve.Comparison.Infrastructure.Data;

public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _database;

    public IMongoCollection<Airport> Airports => _database.GetCollection<Airport>(nameof(Airports).ToLower());
    public IMongoCollection<Vendor> Vendors => _database.GetCollection<Vendor>(nameof(Vendors).ToLower());

    public MongoDbContext(IMongoDatabase database)
    {
        _database = database;

        CreateAirportClauseIndex();
        CreateVendorNameIndex();
        CreateVendorPriorityIndex();
    }

    private void CreateAirportClauseIndex()
    {
        var options = new CreateIndexOptions
        {
            Name = "AirportClauseIndex",
            Collation = new Collation("en", strength: CollationStrength.Primary)
        };
        var indexDefinition = new IndexKeysDefinitionBuilder<Airport>()
            .Combine(
                new IndexKeysDefinitionBuilder<Airport>().Descending(x => x.IsPopular),
                new IndexKeysDefinitionBuilder<Airport>().Ascending(x => x.IataCode),
                new IndexKeysDefinitionBuilder<Airport>().Ascending("City.DisplayNames.Value"),
                new IndexKeysDefinitionBuilder<Airport>().Ascending(x => x.City.Country.Code),
                new IndexKeysDefinitionBuilder<Airport>().Ascending(x => x.Code),
                new IndexKeysDefinitionBuilder<Airport>().Ascending(x => x.Name)
            );
        var indexModel = new CreateIndexModel<Airport>(indexDefinition, options);
        var result = Airports.Indexes.CreateOne(indexModel);

        //var indexDefinition = new IndexKeysDefinitionBuilder<Airport>()
        //    .Text("IataCode")
        //    .Text("City.DisplayNames.Value");

        //var options = new CreateIndexOptions
        //{
        //    Weights = new BsonDocument
        //{
        //    { "IataCode", 80 },
        //    { "City.DisplayNames.Value", 70 }
        //}
        //};

        //var indexModel = new CreateIndexModel<Airport>(indexDefinition, options);
        //var result = Airports.Indexes.CreateOne(indexModel);
    }

    private void CreateVendorNameIndex()
    {
        var options = new CreateIndexOptions { Collation = new Collation("en", strength: CollationStrength.Primary) };
        var indexDefinition = new IndexKeysDefinitionBuilder<Vendor>()
            .Descending(x => x.IsActive)
            .Ascending(x => x.Name);
        var indexModel = new CreateIndexModel<Vendor>(indexDefinition, options);
        var result = Vendors.Indexes.CreateOne(indexModel);
    }

    private void CreateVendorPriorityIndex()
    {
        var options = new CreateIndexOptions { Collation = new Collation("en", strength: CollationStrength.Primary) };
        var indexDefinition = new IndexKeysDefinitionBuilder<Vendor>()
            .Descending(x => x.IsActive)
            .Ascending(x => x.Priority);
        var indexModel = new CreateIndexModel<Vendor>(indexDefinition, options);
        var result = Vendors.Indexes.CreateOne(indexModel);
    }
}