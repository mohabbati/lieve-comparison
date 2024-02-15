using Lieve.Comparison.Core.Entities;
using Lieve.Comparison.Core.Enums;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Lieve.Comparison.Application.Airports.Queries.GetAirports;

public class GetAirportsHandler : IRequestHandler<GetAirports.Request, GetAirports.Response>
{
    private readonly IMongoCollection<Airport> _mongoCollection;

    public GetAirportsHandler(IMongoCollection<Airport> mongoCollection)
    {
        _mongoCollection = mongoCollection;
    }

    public async Task<GetAirports.Response> Handle(GetAirports.Request request, CancellationToken cancellationToken)
    {
        const int iranCode = 101061;

        var filterBuilder = Builders<Airport>.Filter;
        var filter = string.IsNullOrWhiteSpace(request.Clause)
            ? Builders<Airport>.Filter.Eq(x => x.IsPopular, true)
            : Builders<Airport>.Filter.Or(
            Builders<Airport>.Filter.Regex(x => x.IataCode, new BsonRegularExpression(request.Clause, "i")),
            Builders<Airport>.Filter.Regex(x => x.Name, new BsonRegularExpression(request.Clause, "i")),
            Builders<Airport>.Filter.Regex(x => x.City.DisplayNames.Select(s => s.Value), new BsonRegularExpression(request.Clause, "i")),
            Builders<Airport>.Filter.Regex(x => x.City.Country.DisplayNames.Select(s => s.Value), new BsonRegularExpression(request.Clause, "i"))
        );

        filter &= request.LocalityType switch
        {
            LocalityType.Domestic => filterBuilder.Eq(x => x.City.Country.Code, iranCode),
            LocalityType.International => filterBuilder.Ne(x => x.City.Country.Code, iranCode),
            _ => filterBuilder.Empty
        };

        var airports = await _mongoCollection
            .Find(filter)
            .SortByDescending(x => x.IsPopular)
            .Limit(10)
            .ToListAsync(cancellationToken);
        
        var airportsDtos = AirportDto.MapFrom(airports).ToList();
        
        return new GetAirports.Response(airportsDtos);
    }
}