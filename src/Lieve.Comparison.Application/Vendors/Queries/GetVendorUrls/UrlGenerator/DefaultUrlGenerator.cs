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
            .Replace("[FROM]", request.From.ToUpper())
            .Replace("[TO]", request.To.ToUpper())
            .Replace("[ALL?]", string.Empty)
            .Replace("[ALL?]", string.Empty)
            .Replace("[ADL]", request.Adl.ToString())
            .Replace("[CHD]", request.Chd.ToString())
            .Replace("[INF]", request.Inf.ToString())
            .Replace("[YYYY-MM-DD]", DateHelper.ToPersianDate(request.DepartureDate));

        url = request.ReturnDate is not null
            ? url.Replace("[YYYY-MM-DD?]", DateHelper.ToPersianDate(request.ReturnDate))
            : HttpHelper.RemoveParameter(url, "[YYYY-MM-DD?]");

        url = request.CabinClass is not null
            ? url.Replace("[CABINCLASS?]", request.CabinClass.ToString())
            : HttpHelper.RemoveParameter(url, "[CABINCLASS?]");

        return $"{baseUrl}{url}";
    }
}
