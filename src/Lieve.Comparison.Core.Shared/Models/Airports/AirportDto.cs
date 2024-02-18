namespace Lieve.Comparison.Domain.Shared.Models.Airports;

public record AirportDto
{
    public string IataCode { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string CityName { get; set; } = default!;
    public string CountryName { get; set; } = default!;
}