using Lieve.Comparison.WebUi.Client.Models;

namespace Lieve.Comparison.WebUi.Client.Components.FlightCriteria;

public partial class FlightCriteriaComponent
{
    [Parameter]
    public required ServiceType ServiceType { get; set; }

    [Parameter]
    public Flight Flight { get; set; } = new();

    private readonly DeBouncer deBouncer = new(TimeSpan.FromMilliseconds(300));

    [Inject] public required IAirportClient AirportClient { get; set; }

    private async Task<IEnumerable<AirportDto>> AirportLookup(string input, CancellationToken cancellationToken)
    {
        var result = await deBouncer.Debounce<IEnumerable<AirportDto>>(async () =>
        {
            var airports = await AirportClient.GetAsync(ResolveLocalityType(), input ?? string.Empty, cancellationToken);

            return airports;
        });
        
        return result;
    }

    private LocalityType ResolveLocalityType()
    {
        return ServiceType switch
        {
            ServiceType.DomesticFlight => LocalityType.Domestic,
            ServiceType.InternationalFlight => LocalityType.International,
            _ => LocalityType.None,
        };
    }
}
