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
        DateTime departureDate,
        DateTime? returnDate,
        int adl,
        int chd,
        int inf,
        CabinClass? cabinClass,
        CancellationToken cancellationToken = default)
    {
        var vendorQuery = $"vendors={string.Join("&vendors=", vendors)}";
        var returnDateQuery = "";// returnDate is not null ? $"&returnDate={returnDate.Value:o}" : "";
        var cabinClassQuery = cabinClass is not null ? $"&cabinClass={cabinClass.Value}" : "";

        var url = $"api/vendorurl?{vendorQuery}&serviceType={serviceType}&from={from}&to={to}&departureDate={departureDate:o}{returnDateQuery}&adl={adl}&chd={chd}&inf={inf}{cabinClassQuery}";

        var response = await _httpClient.GetFromJsonAsync<IList<VendorUrlDto>>(url, cancellationToken);

        return response ?? [];
    }
}
