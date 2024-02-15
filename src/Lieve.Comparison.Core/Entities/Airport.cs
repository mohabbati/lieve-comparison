using MongoDB.Bson;

namespace Lieve.Comparison.Domain.Entities;

public class Airport
{
    public ObjectId _id { get; set; }
    public required City City { get; set; }
    public required int Code { get; set; }
    public required string IataCode { get; set; }
    public required string Name { get; set; }
    public required List<DisplayName> DisplayNames { get; set; }
    public required string DomainCode { get; set; }
    public required bool IsPopular { get; set; }
}
