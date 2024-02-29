namespace Lieve.Comparison.WebUi.Client.Components.AirportAutocomplete;

public partial class AirportAutocompleteComponent
{
    private KeyValuePair<string, string> _airport;

    [Parameter]
    public required KeyValuePair<string, string> Airport
    {
        get => _airport;
        set
        {
            if (value.Key == _airport.Key && value.Value == _airport.Value) return;
            _airport = value;
            _ = AirportChanged.InvokeAsync(value);
        }
    }

    [Parameter]
    public EventCallback<KeyValuePair<string, string>> AirportChanged { get; set; }

    [Parameter]
    public string Label { get; set; } = default!;

    [Parameter]
    public required Func<string, CancellationToken, Task<IEnumerable<KeyValuePair<string, string>>>> SearchFuncWithCancel { get; set; }

    private static IEnumerable<string> Validate(KeyValuePair<string, string> value)
    {
        if (string.IsNullOrWhiteSpace(value.Key) || string.IsNullOrWhiteSpace(value.Value))
        {
            yield return "The State field is required";
        }
    }
}
