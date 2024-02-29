using System.Globalization;

namespace Lieve.Comparison.Application.Vendors.Queries;

public static class AlibabaUrlGenerator
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
            .Replace("[YYYY-MM-DD]", ToPersianDate(request.DepartureDate));

        if (request.ReturnDate is not null)
        {
            url = url.Replace("[YYYY-MM-DD?]", ToPersianDate(request.ReturnDate));
        }
        else
        {
            url = url.Replace("&returning=[YYYY-MM-DD?]", "");
        }

        if (request.CabinClass is not null && request.CabinClass != CabinClass.Economy)
        {
            url = url.Replace("[CABINCLASS?]", request.CabinClass.ToString());
        }
        else
        {
            url = url.Replace("&flightClass=[CABINCLASS?]", "");
        }

        return $"{baseUrl}{url}";
    }

    private static string ToPersianDate(DateTimeOffset? date)
    {
        if (date is null) return string.Empty;

        var persianCalendar = new PersianCalendar();

        var dateToConvert = date ?? new();

        return $"" +
            $"{persianCalendar.GetYear(dateToConvert.DateTime)}-" +
            $"{persianCalendar.GetMonth(dateToConvert.DateTime).ToString().PadLeft(2, '0')}-" +
            $"{persianCalendar.GetDayOfMonth(dateToConvert.DateTime).ToString().PadLeft(2, '0')}";
    }
}
