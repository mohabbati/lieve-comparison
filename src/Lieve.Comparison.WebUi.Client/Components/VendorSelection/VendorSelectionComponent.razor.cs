namespace Lieve.Comparison.WebUi.Client.Components.VendorSelection;

public partial class VendorSelectionComponent
{
    [Parameter]
    public IList<VendorDto> Vendors { get; set; } = [];

    [Parameter]
    public required ServiceType ServiceType { get; set; }

    [Inject]
    public required IVendorClient VendorClient { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Vendors = await VendorClient.GetAsync(ServiceType.DomesticFlight);
        
        await base.OnInitializedAsync();
    }
}
