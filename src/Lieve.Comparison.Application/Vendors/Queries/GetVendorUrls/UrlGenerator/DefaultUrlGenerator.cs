namespace Lieve.Comparison.Application.Vendors.Queries;

public static class DefaultUrlGenerator
{
    /// <summary>
    /// It generate the vendor navigation url
    /// </summary>
    /// <param name="request">Request consists search clauses</param>
    /// <param name="baseUrl">The vendor base address</param>
    /// <param name="uriTemplate">The vendor uri template should be used to generate the url</param>
    /// <returns></returns>
    public static string Generate(GetVendorUrls.Request request, string baseUrl, string uriTemplate)
    {
        var url = uriTemplate
            .Replace("[FROM]", request.From)
            .Replace("[TO]", request.To)
            .Replace("[ALL?]", string.Empty)
            .Replace("[ALL?]", string.Empty)
            .Replace("[ADL]", request.Adl.ToString())
            .Replace("[CHD]", request.Chd.ToString())
            .Replace("[INF]", request.Inf.ToString())
            .Replace("[YYYY-MM-DD]", request.DepartureDate.ToString("yyyy-MM-dd"))
            .Replace("[YYYY-MM-DD?]", request.ReturnDate?.ToString("yyyy-MM-dd"))
            .Replace("[CABINCLASS]", request.CabinClass.ToString());

        return $"{baseUrl}{url}";
    }
}
