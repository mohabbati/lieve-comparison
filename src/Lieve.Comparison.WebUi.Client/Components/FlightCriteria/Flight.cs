namespace Lieve.Comparison.WebUi.Client.Components.FlightCriteria;

public sealed class Flight
{
    public TripType TripType { get; set; } = TripType.OneWay;
    public KeyValuePair<string, string> Origin { get; set; } = default!;
    public KeyValuePair<string, string> Destination { get; set; } = default!;
    public DateRange DateRange { get; set; } = default!;
    public int Adl { get; set; } = 1;
    public int Chd { get; set; }
    public int Inf { get; set; }
    public CabinClass CabinClass { get; set; } = CabinClass.Economy;
}
