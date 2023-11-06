using System.Text.Json;

namespace FootballPlayers.Common.Extensions;

public static class StringExtensions
{
    public static string ToCamelCase(this string str)
    {
        return JsonNamingPolicy.CamelCase.ConvertName(str);
    }
    
    public static string ToTitleCase(this string? str)
    {
        if (str is null)
            return null;

        if (str.Length > 1)
            return char.ToUpper(str[0]) + str.Substring(1);

        return str.ToUpper();
    }
}
