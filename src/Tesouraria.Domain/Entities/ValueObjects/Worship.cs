namespace Tesouraria.Domain.Entities.ValueObjects;

public class Worship : EntityBase
{
    public DayOfWeek DayOfWeek { get; init; }
    public TimeSpan Time { get; init; }
    public string? Description { get; init; }
}