using Bit.BlazorUI;
using Lieve.Comparison.Domain.Shared.Enums;
using Lieve.Comparison.WebUi.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Lieve.Comparison.WebUi.Client.Components;

public partial class AirportSearch
{
    private SearchModel model = new();

    private readonly List<BitDropdownItem<string>> originAirports = [];
    private readonly List<BitDropdownItem<string>> destinationAirports = [];

    [Inject]
    public required IAirportClient AirportClient { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await SetDropDownList(originAirports, string.Empty);
        await SetDropDownList(destinationAirports, string.Empty);
    }

    private class SearchModel
    {
        public string Origin { get; set; } = default!;
        public string Destination { get; set; } = default!;
        public BitDateRangePickerValue DateRange { get; set; } = default!;
        public string Type { get; set; } = default!;
        public int Adl { get; set; } = 1;
        public int Chd { get; set; }
        public int Inf { get; set; }
    }

    private async Task SetDropDownList(List<BitDropdownItem<string>> dropDownList, string? input)
    {
        var airports = await AirportClient.GetAsync(LocalityType.International, input ?? string.Empty);

        dropDownList.Clear();

        if (string.IsNullOrWhiteSpace(input))
            dropDownList.Add(new() { ItemType = BitDropdownItemType.Header, Text = "Popular" });

        foreach (var item in airports)
        {
            dropDownList.Add(
                new() { Text = $"{item.IataCode}-{item.CountryName}-{item.CityName}-{item.Name}", Value = item.IataCode }
                );
        }
        //new() { ItemType = BitDropdownItemType.Divider }
    }

    private async Task<IEnumerable<string>> Search(string value, CancellationToken token)
    {
        var airports = await AirportClient.GetAsync(LocalityType.International, value ?? string.Empty);

        var result = airports.Select(item => $"{item.IataCode}-{item.CountryName}-{item.CityName}-{item.Name}").ToList();

        return result;
    }

    private async Task Submit()
    {
        //
    }
}
