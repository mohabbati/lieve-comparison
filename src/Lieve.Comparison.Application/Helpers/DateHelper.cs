using System.Globalization;

namespace Lieve.Comparison.Application.Helpers;

public static class DateHelper
{
    public static string ToPersianDate(DateTimeOffset? date)
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
