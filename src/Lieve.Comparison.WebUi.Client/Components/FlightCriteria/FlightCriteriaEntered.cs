namespace Lieve.Comparison.WebUi.Client.Components.FlightCriteria;

public sealed class FlightCriteriaEntered
{
    public KeyValuePair<string, string> Origin { get; set; } = default!;
    public KeyValuePair<string, string> Destination { get; set; } = default!;
    public DateRange DateRange { get; set; } = default!;
    public int Adl { get; set; } = 1;
    public int Chd { get; set; }
    public int Inf { get; set; }
}
