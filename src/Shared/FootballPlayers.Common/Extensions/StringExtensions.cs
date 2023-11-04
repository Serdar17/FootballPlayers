using System.Text.Json;

namespace FootballPlayers.Common.Extensions;

public static class StringExtensions
{
    public static string ToCamelCase(this string str)
    {
        return JsonNamingPolicy.CamelCase.ConvertName(str);
    }
}
