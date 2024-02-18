using Lieve.Comparison.Domain.Shared.Enums;
using Lieve.Comparison.Domain.Shared.Models.Airports;
using Lieve.Comparison.WebUi.Client.Services.Interfaces;
using System.Net.Http.Json;

namespace Lieve.Comparison.WebUi.Client.Services.Implementations;

public class AirportClient : IAirportClient
{
    private readonly HttpClient _httpClient;

    public AirportClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<AirportDto>> GetAsync(LocalityType localityType, string clause, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetFromJsonAsync<List<AirportDto>>($"api/airport?localityType={localityType}&clause={clause}", cancellationToken);

        return response ?? [];
    }
}
