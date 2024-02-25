namespace Lieve.Comparison.WebUi.Client.Services.Interfaces;

public interface IVendorUrlClient
{
    Task<IList<VendorUrlDto>> GetAsync(
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
        CancellationToken cancellationToken = default);
}