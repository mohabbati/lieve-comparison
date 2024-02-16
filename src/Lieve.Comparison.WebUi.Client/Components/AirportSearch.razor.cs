using Lieve.Comparison.Core.Shared.Enums;
using Lieve.Comparison.Core.Shared.Models.Airports;
using Lieve.Comparison.WebUi.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Lieve.Comparison.WebUi.Client.Components;

public partial class AirportSearch
{
    private List<AirportDto>? Airports { get; set; }

    [Inject]
    public required IAirportClient AirportClient { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Airports ??= await GetAirports(string.Empty);
    }

    private async Task<List<AirportDto>> GetAirports(string? input)
    {
        var response = await AirportClient.GetAsync(LocalityType.International, input ?? string.Empty);
        Airports = response;

        return response;
    }
}
