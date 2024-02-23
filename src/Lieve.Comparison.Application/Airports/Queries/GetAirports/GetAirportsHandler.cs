using MongoDB.Bson;
using MongoDB.Driver;

namespace Lieve.Comparison.Application.Airports.Queries;

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
        const int ikaCode = 12000321;

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
            LocalityType.Domestic => filterBuilder
                .Or(Builders<Airport>.Filter.Eq(x => x.City.Country.Code, iranCode),
                    Builders<Airport>.Filter.Ne(x => x.Code, ikaCode)),
            LocalityType.International => filterBuilder
                .Or(Builders<Airport>.Filter.Ne(x => x.City.Country.Code, iranCode),
                    Builders<Airport>.Filter.Eq(x => x.Code, ikaCode)),
            _ => filterBuilder.Empty
        };

        var airports = await _mongoCollection
            .Find(filter)
            .SortByDescending(x => x.IsPopular)
                .ThenBy(x => x.Code)
                .ThenBy(x => x.City.Code)
                .ThenBy(x => x.IataCode)
                .ThenBy(x => x.City.Country.Code)
            .Limit(10)
            .ToListAsync(cancellationToken);

        var airportsDtos = MapFrom(airports).ToList();
        
        return new GetAirports.Response(airportsDtos);
    }

    public static IEnumerable<AirportDto> MapFrom(IEnumerable<Airport> airports)
    {
        return airports.Select(airport =>
            new AirportDto
            {
                IataCode = airport.IataCode,
                Name = airport.Name,
                CityName = airport.City.DisplayNames.FirstOrDefault(x => x.Language == "fa-IR")!.Value,
                CountryName = airport.City.Country.DisplayNames.FirstOrDefault(x => x.Language == "fa-IR")!.Value
            }
        ).ToList();
    }
}