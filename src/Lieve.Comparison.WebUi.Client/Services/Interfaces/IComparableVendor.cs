namespace Lieve.Comparison.WebUi.Client.Services.Interfaces;

public interface IComparableVendor
{
    IList<VendorUrlDto> VendorUrls { get; }

    void AddRange(IList<VendorUrlDto> vendorUrls);
    void Clear();
}
