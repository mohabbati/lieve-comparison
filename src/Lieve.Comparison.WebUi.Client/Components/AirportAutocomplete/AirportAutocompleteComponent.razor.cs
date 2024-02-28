namespace Lieve.Comparison.WebUi.Client.Components.AirportAutocomplete;

public partial class AirportAutocompleteComponent
{
    [Parameter]
    public KeyValuePair<string, string> Airport { get; set; } = default!;

    [Parameter]
    public EventCallback<KeyValuePair<string, string>> AirportChanged { get; set; }

    [Parameter]
    public string Label { get; set; } = default!;

    [Parameter]
    public Func<string, CancellationToken, Task<IEnumerable<KeyValuePair<string, string>>>> SearchFuncWithCancel { get; set; } = default!;

    private static IEnumerable<string> Validate(KeyValuePair<string, string> value)
    {
        if (string.IsNullOrWhiteSpace(value.Key) || string.IsNullOrWhiteSpace(value.Value))
        {
            yield return "The State field is required";
        }
    }
}
