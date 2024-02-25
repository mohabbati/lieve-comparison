namespace Lieve.Comparison.Domain.Entities;

public sealed class Country
{
    public required int Code { get; set; }
    public required string Name { get; set; }
    public required List<DisplayName> DisplayNames { get; set; }
    public required string DomainCode { get; set; }
}
