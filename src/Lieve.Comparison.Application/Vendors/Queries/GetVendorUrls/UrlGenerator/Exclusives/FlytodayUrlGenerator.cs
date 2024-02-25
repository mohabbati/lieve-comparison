namespace Lieve.Comparison.Application.Vendors.Queries;

public static class FlytodayUrlGenerator
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
            .Replace("[FROM]", request.From[..3])
            .Replace("[TO]", request.To[..3])
            .Replace("[ALL]", request.From[^3..].Equals("ALL", StringComparison.CurrentCultureIgnoreCase) ? "1" : "2")
            .Replace("[ALL]", request.To[^3..].Equals("ALL", StringComparison.CurrentCultureIgnoreCase) ? "1" : "2")
            .Replace("[ADL]", request.Adl.ToString())
            .Replace("[CHD]", request.Chd.ToString())
            .Replace("[INF]", request.Inf.ToString())
            .Replace("[YYYY-MM-DD]", request.DepartureDate.ToString("yyyy-MM-dd"))
            .Replace("[CABINCLASS]", GetCabinClass(request.CabinClass));

        if (request.ReturnDate is not null)
        {
            url = url.Replace("[YYYY-MM-DD?]", request.ReturnDate.Value.ToString("yyyy-MM-dd"));
        }
        else
        {
            url = url.Replace("&returnDate=[YYYY-MM-DD?]", "");
        }

        return $"{baseUrl}{url}";
    }

    private static string GetCabinClass(CabinClass? cabinClass)
    {
        return cabinClass switch
        {
            CabinClass.Economy => "1",
            CabinClass.PremiumEconomy => "2",
            CabinClass.Business => "3",
            CabinClass.PremiumBusiness => "4",
            CabinClass.First => "5",
            CabinClass.PremiumFirst => "6",
            _ => string.Empty
        };
    }
}
