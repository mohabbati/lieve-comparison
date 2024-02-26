using Lieve.Comparison.WebUi.Client.Components.FlightCriteria;

namespace Lieve.Comparison.WebUi.Client.Components.CompareCriteria;

public sealed partial class CompareCriteriaComponent
{
    private MudTabs tabs = default!;
    private MudTabPanel domesticFlightPanel = default!;
    private MudTabPanel internationalFlightPanel = default!;
    private MudTabPanel panel03 = default!;

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    private IList<VendorDto> _vendorList;

    public async Task HandleOnSearchSubmitted(FlightCriteriaEventArgs args)
    {
        ArgumentNullException.ThrowIfNull(args);

        NavigationManager.NavigateTo("");

        await Task.CompletedTask;
    }

    private async Task SubmitAsync()
    {
        NavigationManager.NavigateTo("");

        await Task.CompletedTask;
    }
}
