using System.Net.Http.Json;

namespace Lieve.Comparison.WebUi.Client.Services.Implementations;

public class VendorUrlClient : IVendorUrlClient
{
    private readonly HttpClient _httpClient;

    public VendorUrlClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IList<VendorUrlDto>> GetAsync(
        string[] vendors,
        ServiceType serviceType,
        string from,
        string to,
        DateTimeOffset departureDate,
        DateTimeOffset? returnDate,
        short adl,
        short chd,
        short inf,
        CabinClass? cabinClass,
        CancellationToken cancellationToken = default)
    {
        var vendorQuery = $"vendors={string.Join("&vendors=", vendors)}";
        var returnDateQuery = returnDate.HasValue ? $"&returnDate={returnDate.Value:o}" : "";
        var cabinClassQuery = cabinClass.HasValue ? $"&cabinClass={cabinClass.Value}" : "";

        var url = $"api/vendor?{vendorQuery}&serviceType={serviceType}&from={from}&to={to}&departureDate={departureDate:o}{returnDateQuery}&adl={adl}&chd={chd}&inf={inf}{cabinClassQuery}";

        var response = await _httpClient.GetFromJsonAsync<IList<VendorUrlDto>>(url, cancellationToken);

        return response ?? [];
    }
}
