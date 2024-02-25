using MongoDB.Driver;

namespace Lieve.Comparison.Application.Interfaces;

public interface IMongoDbContext
{
    IMongoCollection<Airport> Airports { get; }
    IMongoCollection<Vendor> Vendors { get; }
}

