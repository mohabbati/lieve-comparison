using Lieve.Comparison.Application.Airports;
using Lieve.Comparison.Core.Shared.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lieve.Comparison.WebUi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AirportController : ControllerBase
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
