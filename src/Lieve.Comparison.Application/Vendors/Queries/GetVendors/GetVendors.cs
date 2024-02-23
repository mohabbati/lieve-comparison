namespace Lieve.Comparison.Application.Vendors.Queries;

public abstract class GetVendors
{
    public record Request(ServiceType ServiceType) : IRequest<Response>;

    public record Response(List<VendorDto> VendorDtos);
}
