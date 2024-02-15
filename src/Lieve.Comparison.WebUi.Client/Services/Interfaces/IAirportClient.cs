using Lieve.Comparison.Application.Airports.Queries.GetAirports;
using Lieve.Comparison.Core.Enums;

namespace Lieve.Comparison.WebUi.Client.Services.Interfaces;

public interface IAirportClient
{
    Task<List<AirportDto>> GetAsync(LocalityType localityType, string clause, CancellationToken cancellationToken = default);
}
