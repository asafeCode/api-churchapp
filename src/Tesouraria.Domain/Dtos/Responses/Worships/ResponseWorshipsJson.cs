namespace Tesouraria.Domain.Dtos.Responses.Worships;

public record ResponseWorshipsJson
{
    public IEnumerable<ResponseWorshipJson> Worships { get; init; } = [];
}
public record ResponseWorshipJson
{
    public Guid Id { get; init; }
    public DayOfWeek DayOfWeek { get; init; }
    public TimeSpan Time { get; init; }
    public string? Description { get; init; }
}