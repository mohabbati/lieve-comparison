﻿using Lieve.Comparison.WebUi.Client.Components.FlightCriteria;

namespace Lieve.Comparison.WebUi.Client.Components.CompareCriteria;

public sealed partial class CompareCriteriaComponent
{
    private MudTabs _tabs = default!;
    private MudForm _flightForm = default!;
    private Flight _flight = new();
    private IList<VendorDto> _vendors = [];

    [Inject] public required IVendorClient VendorClient { get; set; }

    [Inject] public required IVendorUrlClient VendorUrlClient { get; set; }

    [Inject] public required NavigationManager NavigationManager { get; set; }

    [Inject] private IDialogService DialogService { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await SetVendorsAsync();
            StateHasChanged();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task SubmitAsync()
    {
        await _flightForm.Validate();

        if (_flightForm.IsValid is false) return;

        var selectedVendors = _vendors.Where(x => x.IsSelected).ToList();

        if (selectedVendors.Count == 0)
        {
            bool? result = await DialogService.ShowMessageBox(
                "Warning",
                "1 to 4 vendors should be selected!",
                options: new DialogOptions()
                { Position = DialogPosition.Center }
                );

            return;
        }

        var vendorUrls = await VendorUrlClient.GetAsync(
            selectedVendors.Select(x => x.Name).ToArray(), ResolveServiceType(), _flight.Origin.Key, _flight.Destination.Key, _flight.DateRange.Start!.Value, null, _flight.Adl, _flight.Chd, _flight.Inf, _flight.CabinClass);

        var c = vendorUrls.Select(x => x.NavigationUrl).FirstOrDefault()!;
        NavigationManager.NavigateTo(c);

        await Task.CompletedTask;
    }

    private async Task SetVendorsAsync()
    {
        _vendors = await VendorClient.GetAsync(ResolveServiceType(), CancellationToken.None);
    }

    private ServiceType ResolveServiceType()
    {
        return _tabs.ActivePanelIndex switch
        {
            0 => ServiceType.DomesticFlight,
            1 => ServiceType.InternationalFlight,
            _ => ServiceType.None,
        };
    }
}
