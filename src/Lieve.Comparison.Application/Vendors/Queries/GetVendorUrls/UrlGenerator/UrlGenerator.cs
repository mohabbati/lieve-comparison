namespace Lieve.Comparison.Application.Vendors.Queries;

public static class UrlGenerator
{
    public static readonly Dictionary<string, Func<GetVendorUrls.Request, string, string, string>> Strategies = new()
    {
        { "alibaba", AlibabaUrlGenerator.Generate },
        { "flytoday", FlytodayUrlGenerator.Generate }
    };
}