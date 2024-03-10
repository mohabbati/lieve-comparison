using System.Text.RegularExpressions;

namespace Lieve.Comparison.Application.Helpers;

public static class HttpHelper
{
    public static string RemoveParameter(string queryString, string parameterPattern)
    {
        var pattern = @"&[^&]*?" + Regex.Escape(parameterPattern);

        var result = Regex.Replace(queryString, pattern, string.Empty);

        return result;
    }
}
