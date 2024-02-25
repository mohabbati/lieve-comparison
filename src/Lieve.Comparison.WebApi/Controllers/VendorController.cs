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
}
