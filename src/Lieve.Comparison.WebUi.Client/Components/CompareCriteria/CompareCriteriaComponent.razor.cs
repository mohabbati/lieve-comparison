using Lieve.Comparison.WebUi.Client.Components.FlightCriteria;

namespace Lieve.Comparison.WebUi.Client.Components.CompareCriteria;

public sealed partial class CompareCriteriaComponent
{
    private MudTabs _tabs = default!;
    private MudForm _flightForm = default!;
    private Flight _flight = new();
    private IList<VendorDto> _vendors = [];

    [Inject]
    public required IVendorClient VendorClient { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

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
        
        var a = _vendors.Any(x => x.IsSelected);

        NavigationManager.NavigateTo("");

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
