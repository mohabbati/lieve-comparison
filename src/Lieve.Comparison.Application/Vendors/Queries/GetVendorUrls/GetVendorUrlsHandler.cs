using Lieve.Comparison.Application.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Lieve.Comparison.Application.Vendors.Queries;

public sealed class GetVendorUrlsHandler : IRequestHandler<GetVendorUrls.Request, GetVendorUrls.Response>
{
    private readonly IMongoDbContext _dbContext;

    public GetVendorUrlsHandler(IMongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetVendorUrls.Response> Handle(GetVendorUrls.Request request, CancellationToken cancellationToken)
    {
        var filter = Builders<Vendor>.Filter.Regex(x => x.Name,
            new BsonRegularExpression(string.Join("|", request.Vendors), "i"));

        var vendors = await _dbContext.Vendors
            .Find(filter)
            .ToListAsync(cancellationToken);

        IList<VendorUrlDto> vendorUrlDtos = [];

        foreach (var vendor in vendors)
        {
            if (UrlGenerator.Strategies.TryGetValue(vendor.Name.ToLower(), out var strategy) is false)
            {
                strategy = DefaultUrlGenerator.Generate;
            }

            var uriTemplate = vendor.ProvidedServices.FirstOrDefault(x => x.ServiceType == request.ServiceType)?.UriTemplate;
            if (uriTemplate is null) continue;
            var url = strategy(request, vendor.BaseUrl, uriTemplate);

            vendorUrlDtos.Add(new VendorUrlDto
            {
                Name = vendor.Name,
                NavigationUrl = url,
                LogoUri = vendor.LogoUri,
                FavIconUri = vendor.FavIconUri,
            });
        }

        return new GetVendorUrls.Response(vendorUrlDtos);
    }
}