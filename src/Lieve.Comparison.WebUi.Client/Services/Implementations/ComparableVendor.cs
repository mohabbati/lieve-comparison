namespace Lieve.Comparison.WebUi.Client.Services.Implementations;

public sealed class ComparableVendor : IComparableVendor
{
    public IList<VendorUrlDto> VendorUrls { get; } = [];

    public void AddRange(IList<VendorUrlDto> vendorUrls)
    {
        foreach (var item in vendorUrls)
        {
            VendorUrls.Add(item);
        }
    }

    public void Clear()
    {
        VendorUrls.Clear();
    }
}
