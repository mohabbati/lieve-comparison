using Lieve.Comparison.WebUi.Client.Components.FlightCriteria;

namespace Lieve.Comparison.WebUi.Client.Components.CompareCriteria;

public sealed partial class CompareCriteriaComponent
{
    private readonly FlightCriteriaEntered _model = new();
    private MudForm _form = default!;
    private MudTabs _tabs = default!;
    private IList<VendorDto> _vendorList = [];

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

    private async Task SetVendorsAsync()
    {
        _vendorList = await VendorClient.GetAsync(ResolveServiceType(), CancellationToken.None);
    }

    private async Task SubmitAsync()
    {
        await _form.Validate();

        if (_form.IsValid is false) return;
        
        var a = _vendorList.Any(x => x.IsSelected);

        NavigationManager.NavigateTo("");

        await Task.CompletedTask;
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
