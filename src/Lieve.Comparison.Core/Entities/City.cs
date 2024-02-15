namespace Lieve.Comparison.Core.Entities;

public class City
{
    public int Code { get; set; }
    public required string Name { get; set; }
    public required List<DisplayName> DisplayNames { get; set; }
    public required string DomainCode { get; set; }
    public required Country Country { get; set; }
}
