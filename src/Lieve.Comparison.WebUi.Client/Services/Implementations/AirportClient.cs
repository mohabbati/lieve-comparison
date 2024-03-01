using System.Net.Http.Json;

namespace Lieve.Comparison.WebUi.Client.Services.Implementations;

public sealed class AirportClient : IAirportClient
{
    private readonly HttpClient _httpClient;

    public AirportClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IList<AirportDto>> GetAsync(LocalityType localityType, string clause, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetFromJsonAsync<IList<AirportDto>>($"api/airport?localityType={localityType}&clause={clause}", cancellationToken);

        return response ?? [];
    }
}
