namespace Lieve.Comparison.Application.Vendors.Queries;

public abstract class GetVendorUrls
{
    public record Request(
        IList<string> Vendors,
        ServiceType ServiceType,
        string From,
        string To,
        DateTimeOffset DepartureDate,
        DateTimeOffset? ReturnDate,
        short Adl,
        short Chd,
        short Inf,
        CabinClass? CabinClass) : IRequest<Response>;

    public record Response(IList<VendorUrlDto> VendorUrlDtos);
}
