using System.Globalization;

namespace Tesouraria.Domain.Extensions;

public static class DateExtensions
{
    public static DateOnly ToDateOnly(this DateTime date) => DateOnly.FromDateTime(date);

    public static string ToPortuguese(this DayOfWeek? day)
    {
        if (day is null)
            return "—";
        
        var culture = new CultureInfo("pt-BR");

        var dayReturn = culture.DateTimeFormat
            .GetDayName(day.Value)
            .Replace("-feira", "")
            .Trim();

        return char.ToUpper(dayReturn[0]) + dayReturn[1..];
    }
}