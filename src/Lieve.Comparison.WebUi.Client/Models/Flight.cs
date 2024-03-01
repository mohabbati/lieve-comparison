namespace Lieve.Comparison.WebUi.Client.Models;

public sealed record Flight
{
    public TripType TripType { get; set; } = TripType.OneWay;
    public AirportDto Origin { get; set; } = default!;
    public AirportDto Destination { get; set; } = default!;
    public DateRange DateRange { get; set; } = default!;
    public int Adl { get; set; } = 1;
    public int Chd { get; set; }
    public int Inf { get; set; }
    public CabinClass CabinClass { get; set; } = CabinClass.Economy;
}
