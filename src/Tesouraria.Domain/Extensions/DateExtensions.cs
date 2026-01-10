namespace Tesouraria.Domain.Extensions;

public static class DateExtensions
{
    public static DateOnly ToDateOnly(this DateTime date) => DateOnly.FromDateTime(date);
}