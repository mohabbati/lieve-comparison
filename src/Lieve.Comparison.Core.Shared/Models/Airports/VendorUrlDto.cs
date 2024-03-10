namespace Lieve.Comparison.Domain.Shared.Models;

public record VendorUrlDto
{
    public string Name { get; set; } = default!;
    public string NavigationUrl { get; set; } = default!;
    public string LogoUri { get; set; } = default!;
    public string FavIconUri { get; set; } = default!;
}