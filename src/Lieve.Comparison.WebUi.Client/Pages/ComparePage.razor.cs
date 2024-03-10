namespace Lieve.Comparison.WebUi.Client.Pages;

public partial class ComparePage
{
    private MudTabs _tabs = default!;

    [Inject] public IComparableVendor ComparableVendor { get; set; } = default!;

    private static string SanetizeFavIconUrl(string iconUri)
    {
        var result = $"<image width=\"20\" height=\"20\" xlink:href=\"{iconUri}\" />";

        return result;
    }
}
