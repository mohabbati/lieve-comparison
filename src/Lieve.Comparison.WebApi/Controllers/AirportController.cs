using Lieve.Comparison.Application.Airports.Queries;

namespace Lieve.Comparison.WebUi.Controllers;

public class AirportController : CustomControllerBase
{
    private readonly IMediator _mediator;

    public AirportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] LocalityType localityType,
        [FromQuery] string? clause)
    {
        var response = await _mediator.Send(new GetAirports.Request(localityType, clause ?? string.Empty));

        return Ok(response.AirportDtos);
    }
}
