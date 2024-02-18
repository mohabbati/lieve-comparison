using Lieve.Comparison.Domain.Shared.Enums;
using Lieve.Comparison.Domain.Shared.Models.Airports;

namespace Lieve.Comparison.WebUi.Client.Services.Interfaces;

public interface IAirportClient
{
    Task<List<AirportDto>> GetAsync(LocalityType localityType, string clause, CancellationToken cancellationToken = default);
}
