using Lieve.Comparison.WebUi.Client.Components.FlightCriteria;
using Microsoft.AspNetCore.Components;

namespace Lieve.Comparison.WebUi.Components.Pages;

public partial class Home
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    //[Inject]
    //public required IVendorService VendorService { get; set; }

    public async Task HandleOnSearchSubmitted(FlightCriteriaEventArgs args)
    {
        ArgumentNullException.ThrowIfNull(args);

        //var url = VendorService.GenerateNavigationUrl();

        NavigationManager.NavigateTo("");

        await Task.CompletedTask;
    }
}
