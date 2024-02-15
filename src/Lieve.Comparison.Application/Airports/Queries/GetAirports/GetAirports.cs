namespace Lieve.Comparison.Application.Airports.Queries.GetAirports;

public abstract class GetAirports
{
    public record Request(string Clause, bool IsDomestic) : IRequest<Response>;

    public record Response(List<AirportDto> AirportDtos);
}