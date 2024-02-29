using Lieve.Comparison.Application.Vendors.Queries;
using Lieve.Comparison.Domain.Shared.Models;

namespace Lieve.Comparison.WebUi.Controllers;

public sealed class VendorUrlController : CustomControllerBase
{
    private readonly IMediator _mediator;

    public VendorUrlController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<VendorUrlDto>>> Get(
        [FromQuery] string[] vendors,
        [FromQuery] ServiceType serviceType,
        [FromQuery] string from,
        [FromQuery] string to,
        [FromQuery] DateTime departureDate,
        [FromQuery] DateTime? returnDate,
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
