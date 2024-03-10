namespace Lieve.Comparison.Application.Vendors.Queries;

public static class SnapptripUrlGenerator
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
            .Replace("[FROM]", request.From[..3].ToUpper())
            .Replace("[TO]", request.To[..3].ToUpper())
            .Replace("[FROMCITY]", request.FromCity)
            .Replace("[TOCITY]", request.ToCity)
            .Replace("[TOCITY]", request.ToCity)
            .Replace("originIsCity=[ISALL]", request.From[^3..].Equals("ALL", StringComparison.CurrentCultureIgnoreCase) ? "originIsCity=true" : "originIsCity=false")
            .Replace("destinationIsCity=[ISALL]", request.To[^3..].Equals("ALL", StringComparison.CurrentCultureIgnoreCase) ? "destinationIsCity=true" : "destinationIsCity=false")
            .Replace("[ADL]", request.Adl.ToString())
            .Replace("[CHD]", request.Chd.ToString())
            .Replace("[INF]", request.Inf.ToString())
            .Replace("[YYYY-MM-DD]", request.DepartureDate.ToString("yyyy-MM-dd"))
            .Replace("[CABINCLASS]", GetCabinClass(request.CabinClass))
            .Replace("[ISROUNDTRIP]", request.ReturnDate is not null ? "true" : "false")
            .Replace("[TRIPTYPE]", GetTripType(request.ReturnDate));

        url = request.ReturnDate is not null
            ? url.Replace("[YYYY-MM-DD?]", request.ReturnDate.Value.ToString("yyyy-MM-dd"))
            : HttpHelper.RemoveParameter(url, "[YYYY-MM-DD?]");

        return $"{baseUrl}{url}";
    }

    private static string GetCabinClass(CabinClass? cabinClass)
    {
        return cabinClass switch
        {
            CabinClass.Economy => "ECONOMY",
            CabinClass.PremiumEconomy => "ECONOMY",
            CabinClass.Business => "BUSINESS",
            CabinClass.PremiumBusiness => "BUSINESS",
            CabinClass.First => "FIRST_CLASS",
            CabinClass.PremiumFirst => "FIRST_CLASS",
            _ => string.Empty
        };
    }

    private static string GetTripType(DateTime? returnDate)
    {
        return returnDate is null ? "oneway" : "roundtrip";
    }
}
