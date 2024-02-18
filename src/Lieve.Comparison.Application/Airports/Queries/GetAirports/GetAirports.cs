using Lieve.Comparison.Domain.Shared.Enums;
using Lieve.Comparison.Domain.Shared.Models.Airports;

namespace Lieve.Comparison.Application.Airports;

public abstract class GetAirports
{
    public record Request(LocalityType LocalityType, string Clause) : IRequest<Response>;

    public record Response(List<AirportDto> AirportDtos);
}