using MongoDB.Driver;

namespace Lieve.Comparison.Application.Vendors.Queries;

public class GetVendorsHandler : IRequestHandler<GetVendors.Request, GetVendors.Response>
{
    private readonly IMongoCollection<Vendor> _mongoCollection;

    public GetVendorsHandler(IMongoCollection<Vendor> mongoCollection)
    {
        _mongoCollection = mongoCollection;
    }

    public async Task<GetVendors.Response> Handle(GetVendors.Request request, CancellationToken cancellationToken)
    {
        var filter = Builders<Vendor>.Filter
            .ElemMatch(v => v.ProvidedServices, ps => ps.ServiceType == request.ServiceType);

        var vendors = await _mongoCollection
            .Find(filter)
            .ToListAsync(cancellationToken);

        var vendorsDtos = MapFrom(vendors).ToList();

        return new GetVendors.Response(vendorsDtos);
    }

    public static IEnumerable<VendorDto> MapFrom(IEnumerable<Vendor> vendors)
    {
        return vendors.Select(vendor =>
            new VendorDto
            {
                Name = vendor.Name,
                DisplayName = vendor.DisplayName
            }
        ).ToList();
    }
}