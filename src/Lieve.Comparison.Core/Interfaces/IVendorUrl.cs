namespace Lieve.Comparison.Domain.Interfaces;

public interface IVendorUrl
{
    Task<Uri> CreateNavigationUrlAsync(string baseUrl, string urlTemplate, CancellationToken cancellationToken);
}
