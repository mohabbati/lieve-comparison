using Lieve.Comparison.Core.Shared.Enums;
using Lieve.Comparison.Core.Shared.Models.Airports;

namespace Lieve.Comparison.WebUi.Client.Services.Interfaces;

public interface IAirportClient
{
    Task<List<AirportDto>> GetAsync(LocalityType localityType, string clause, CancellationToken cancellationToken = default);
}
