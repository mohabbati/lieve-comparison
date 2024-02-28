namespace Lieve.Comparison.WebUi.Client.Services.Interfaces;

public interface IVendorClient
{
    Task<IList<VendorDto>> GetAsync(ServiceType serviceType, CancellationToken cancellationToken);
}
