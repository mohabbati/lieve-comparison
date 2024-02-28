namespace Lieve.Comparison.WebUi.Client.Components.FlightCriteria;

public partial class FlightCriteriaComponent
{
    private readonly FlightCriteriaEntered _model = new();
    private readonly DeBouncer deBouncer = new(TimeSpan.FromMilliseconds(300));

    [Inject]
    public required IAirportClient AirportClient { get; set; }


    private async Task<IEnumerable<KeyValuePair<string, string>>> AirportLookup(string input, CancellationToken cancellationToken)
    {
        var result = await deBouncer.Debounce<IEnumerable<KeyValuePair<string, string>>>(async () =>
        {
            var airports = await AirportClient.GetAsync(LocalityType.International, input ?? string.Empty, cancellationToken);

            var keyValuePairs = airports.Select(x => new KeyValuePair<string, string>(x.IataCode, $"{x.CountryName} - {x.CityName}")).ToList();

            return keyValuePairs;
        });
        
        return result;
    }
}
