using Lieve.Comparison.Application.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Lieve.Comparison.Application.Airports.Queries;

public sealed class GetAirportsHandler : IRequestHandler<GetAirports.Request, GetAirports.Response>
{
    private readonly IMongoDbContext _dbContext;

    public GetAirportsHandler(IMongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetAirports.Response> Handle(GetAirports.Request request, CancellationToken cancellationToken)
    {
        if (request.LocalityType == LocalityType.None)
            return new GetAirports.Response([]);

        var clauseFilter = string.IsNullOrWhiteSpace(request.Clause)
            ? Builders<Airport>.Filter.Eq(x => x.IsPopular, true)
            : Builders<Airport>.Filter.Or(
            Builders<Airport>.Filter.Regex(x => x.IataCode, new BsonRegularExpression(request.Clause, "i")),
            Builders<Airport>.Filter.Regex(x => x.Name, new BsonRegularExpression(request.Clause, "i")),
            Builders<Airport>.Filter.Regex(x => x.City.DisplayNames.Select(s => s.Value), new BsonRegularExpression(request.Clause, "i")),
            Builders<Airport>.Filter.Regex(x => x.City.Country.DisplayNames.Select(s => s.Value), new BsonRegularExpression(request.Clause, "i"))
        );

        var localityFilter = request.LocalityType switch
        {
            LocalityType.Domestic => Builders<Airport>.Filter.And(
                Builders<Airport>.Filter.Eq(x => x.City.Country.DomainCode, "IRN"),
                Builders<Airport>.Filter.Ne(x => x.DomainCode, "IKA")),
            LocalityType.International => Builders<Airport>.Filter.Or(
                Builders<Airport>.Filter.Ne(x => x.City.Country.DomainCode, "IRN"),
                Builders<Airport>.Filter.Eq(x => x.DomainCode, "IKA")),
            _ => Builders<Airport>.Filter.Empty
        };

        var filter = Builders<Airport>.Filter.And(clauseFilter, localityFilter);

        var airports = await _dbContext.Airports
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
                Name = airport.DisplayNames.FirstOrDefault(x => x.Language == "fa-IR")!.Value,
                CityName = airport.City.DisplayNames.FirstOrDefault(x => x.Language == "fa-IR")!.Value,
                CountryName = airport.City.Country.DisplayNames.FirstOrDefault(x => x.Language == "fa-IR")!.Value
            }
        ).ToList();
    }
}