namespace Lieve.Comparison.WebUi.Client.Pages;

public partial class ComparePage
{
    private MudTabs _tabs = default!;

    [Inject] public IComparableVendor ComparableVendor { get; set; } = default!;
}
