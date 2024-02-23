namespace Lieve.Comparison.Domain.Shared.Models;

public record VendorDto
{
    public string Name { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string NavigationUrl { get; set; } = default!;
    public bool IsSelected { get; set; }
}