
using MongoDB.Bson;

namespace Lieve.Comparison.Domain.Entities;

public sealed class Vendor
{
    public ObjectId _id { get; set; }
    public required string Name { get; set; }
    public required string DisplayName { get; set; }
    public required short Priority { get; set; }
    public required string BaseUrl { get; set; }
    public required string LogoUri { get; set; }
    public required string FavIconUri { get; set; }
    public required bool IsActive { get; set; }
    public required ICollection<ProvidedService> ProvidedServices { get; set; }
}
