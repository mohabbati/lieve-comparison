namespace Lieve.Comparison.WebUi.Client.Components.AirportAutocomplete;

public partial class AirportAutocompleteComponent
{
    private AirportDto? _airport;

    [Parameter]
    public required AirportDto? Airport
    {
        get => _airport;
        set
        {
            if (value?.IataCode == _airport?.IataCode && value?.Name == _airport?.Name &&
                value?.CityName == _airport?.CityName && value?.CountryName == _airport?.CountryName) return;

            _airport = value;
            _ = AirportChanged.InvokeAsync(value);
        }
    }

    [Parameter]
    public EventCallback<AirportDto> AirportChanged { get; set; }

    [Parameter]
    public string Label { get; set; } = default!;

    [Parameter]
    public required Func<string, CancellationToken, Task<IEnumerable<AirportDto>>> SearchFuncWithCancel { get; set; }

    private static IEnumerable<string> Validate(AirportDto airport)
    {
        if (string.IsNullOrWhiteSpace(airport.IataCode))
        {
            yield return "The State field is required";
        }
    }
}
