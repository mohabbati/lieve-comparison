using Lieve.Comparison.WebUi.Client.Components.FlightCriteria;

namespace Lieve.Comparison.WebUi.Client.Components.CompareCriteria;

public partial class CompareCriteriaComponent
{
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
