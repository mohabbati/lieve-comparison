using Lieve.Comparison.Application.Interfaces;
using MongoDB.Driver;

namespace Lieve.Comparison.Application.Vendors.Queries;

public sealed class GetVendorsHandler : IRequestHandler<GetVendors.Request, GetVendors.Response>
{
    private readonly IMongoDbContext _dbContext;

    public GetVendorsHandler(IMongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetVendors.Response> Handle(GetVendors.Request request, CancellationToken cancellationToken)
    {
        var filter = Builders<Vendor>.Filter.And(
            Builders<Vendor>.Filter.Eq(x => x.IsActive, true),
            Builders<Vendor>.Filter.ElemMatch(v => v.ProvidedServices, ps => ps.ServiceType == request.ServiceType));

        var vendors = await _dbContext.Vendors
            .Find(filter)
            .SortBy(x => x.Priority)
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