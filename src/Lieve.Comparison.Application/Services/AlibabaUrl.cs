using Lieve.Comparison.Domain.Interfaces;

namespace Lieve.Comparison.Application.Services;

public class AlibabaUrl : IVendorUrl
{
    const string VendorName = "alibaba";

    public Task<Uri> CreateNavigationUrlAsync(string baseUrl, string urlTemplate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
