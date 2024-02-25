using Lieve.Comparison.Application.Vendors.Queries;
using Lieve.Comparison.Domain.Shared.Models;

namespace Lieve.Comparison.WebUi.Controllers;

public sealed class VendorController : CustomControllerBase
{
    private readonly IMediator _mediator;

    public VendorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<VendorDto>>> Get(
        [FromQuery] ServiceType serviceType)
    {
        var response = await _mediator.Send(new GetVendors.Request(serviceType));

        return Ok(response.VendorDtos);
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<List<VendorUrlDto>>> GetUrls(
        [FromQuery] string[] vendors,
        [FromQuery] ServiceType serviceType,
        [FromQuery] string from,
        [FromQuery] string to,
        [FromQuery] DateTimeOffset departureDate,
        [FromQuery] DateTimeOffset? returnDate,
        [FromQuery] short adl,
        [FromQuery] short chd,
        [FromQuery] short inf,
        [FromQuery] CabinClass? cabinClass)
    {
        var response = await _mediator.Send(
            new GetVendorUrls.Request
            (
                vendors,
                serviceType,
                from,
                to,
                departureDate,
                returnDate,
                adl,
                chd,
                inf,
                cabinClass
            ));

        return Ok(response.VendorUrlDtos);
    }
}
