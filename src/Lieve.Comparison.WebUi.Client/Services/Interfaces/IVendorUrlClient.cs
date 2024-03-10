namespace Lieve.Comparison.WebUi.Client.Services.Interfaces;

public interface IVendorUrlClient
{
    Task<IList<VendorUrlDto>> GetAsync(
        string[] vendors,
        ServiceType serviceType,
        string from,
        string to,
        string fromCity,
        string toCity,
        DateTime departureDate,
        DateTime? returnDate,
        int adl,
        int chd,
        int inf,
        CabinClass? cabinClass,
        CancellationToken cancellationToken = default);
}