namespace Lieve.Comparison.WebUi.Client.Components.FlightCriteria;

public partial class FlightCriteriaComponent
{
    [Parameter]
    public ServiceType ServiceType { get; set; }

    [Parameter]
    public Flight Flight { get; set; } = new();

    private readonly DeBouncer deBouncer = new(TimeSpan.FromMilliseconds(300));

    [Inject]
    public required IAirportClient AirportClient { get; set; }

    private async Task<IEnumerable<KeyValuePair<string, string>>> AirportLookup(string input, CancellationToken cancellationToken)
    {
        var result = await deBouncer.Debounce<IEnumerable<KeyValuePair<string, string>>>(async () =>
        {
            var airports = await AirportClient.GetAsync(ResolveLocalityType(), input ?? string.Empty, cancellationToken);

            var keyValuePairs = airports.Select(x => new KeyValuePair<string, string>(x.IataCode, $"{x.CountryName} - {x.CityName}")).ToList();

            return keyValuePairs;
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
