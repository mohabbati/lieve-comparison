using Lieve.Comparison.Core.Enums;

namespace Lieve.Comparison.Application.Airports.Queries.GetAirports;

public abstract class GetAirports
{
    public record Request(LocalityType LocalityType, string Clause) : IRequest<Response>;

    public record Response(List<AirportDto> AirportDtos);
}