using Lieve.Comparison.Core.Entities;

namespace Lieve.Comparison.Application.Airports.Queries.GetAirports;

public record AirportDto(string IataCode, string Name, string CityName, string CountryName)
{
    public static IEnumerable<AirportDto> MapFrom(IEnumerable<Airport> airports)
    {
        return airports.Select(airport => 
            new AirportDto(
                airport.IataCode,
                airport.Name,
                airport.City.DisplayNames.FirstOrDefault(x => x.Language == "fa-IR").Value,
                airport.City.Country.DisplayNames.FirstOrDefault(x => x.Language == "fa-IR").Value)
        ).ToList();
    }
}