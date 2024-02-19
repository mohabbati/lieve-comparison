namespace Lieve.Comparison.WebUi.Client.Components.FlightCriteria;

public class FlightCriteriaEntered
{
    public string Origin { get; set; } = default!;
    public string Destination { get; set; } = default!;
    public DateRange? DateRange { get; set; }
    public int Adl { get; set; } = 1;
    public int Chd { get; set; }
    public int Inf { get; set; }
}
