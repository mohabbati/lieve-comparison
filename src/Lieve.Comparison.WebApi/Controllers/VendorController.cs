using Lieve.Comparison.Application.Vendors.Queries;

namespace Lieve.Comparison.WebUi.Controllers;

public class VendorController : CustomControllerBase
{
    private readonly IMediator _mediator;

    public VendorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] ServiceType serviceType)
    {
        var response = await _mediator.Send(new GetVendors.Request(serviceType));

        return Ok(response.VendorDtos);
    }
}
