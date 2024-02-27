namespace Lieve.Comparison.WebUi.Client.Components.VendorSelection;

public sealed partial class VendorSelectionComponent
{
    [Parameter]
    public IList<VendorDto> Vendors { get; set; } = [];
}
