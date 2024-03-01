using System.Net.Http.Json;

namespace Lieve.Comparison.WebUi.Client.Services.Implementations;

public sealed class VendorClient : IVendorClient
{
    private readonly HttpClient _httpClient;

    public VendorClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IList<VendorDto>> GetAsync(ServiceType serviceType, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetFromJsonAsync<IList<VendorDto>>($"api/vendor?serviceType={serviceType.ToString()}", cancellationToken);

        return response ?? [];
    }
}
