using System.Globalization;

namespace Lieve.Comparison.WebUi.Client.Components.FlightCriteria;

public partial class FlightCriteriaComponent
{
    private MudForm _form = default!;
    private MudDateRangePicker _dateRangePicker = default!;

    private readonly FlightCriteriaEntered model = new();

    [Parameter]
    public EventCallback<FlightCriteriaEventArgs> OnSearchSubmitted{ get; set; }
    public CultureInfo CultureInfo { get; set; } = new("fa-IR");

    [Inject]
    public required IAirportClient AirportClient { get; set; }

    private async Task SubmitAsync()
    {
        await _form.Validate();

        if (_form.IsValid)
        {
            var args = new FlightCriteriaEventArgs { FlightSearchModel = model };
            await OnSearchSubmitted.InvokeAsync(args);
        }
    }

    private async Task<IEnumerable<KeyValuePair<string, string>>> AirportLookup(string input, CancellationToken cancellationToken)
    {
        var airports = await AirportClient.GetAsync(LocalityType.International, input ?? string.Empty, cancellationToken);

        var result = airports.Select(x => new KeyValuePair<string, string>(x.IataCode, $"{x.IataCode}-{x.CountryName}-{x.CityName}-{x.Name}")).ToList();

        return result;
    }

    private static IEnumerable<string> Validate(KeyValuePair<string, string> value)
    {
        if (string.IsNullOrWhiteSpace(value.Key) || string.IsNullOrWhiteSpace(value.Value))
        {
            yield return "The State field is required";
        }
    }

    private DateTime? lastPicker;
    private void ChangeCulture()
    {
        lastPicker ??= _dateRangePicker.PickerMonth ?? DateTime.Now;

        if (CultureInfo.Name == "fa-IR")
        {
            CultureInfo = new CultureInfo("en-US");
        }
        else
        {
            CultureInfo = new CultureInfo("fa-IR");
        }
        lastPicker = null;
    }
}
