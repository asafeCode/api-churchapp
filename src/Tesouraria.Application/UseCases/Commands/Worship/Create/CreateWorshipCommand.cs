using Tesouraria.Domain.Abstractions.Mediator;

namespace Tesouraria.Application.UseCases.Commands.Worship.Create;

public class CreateWorshipCommand : ICommand
{
    public DayOfWeek DayOfWeek { get; init; }
    public TimeSpan Time { get; init; }
    public string? Description { get; init; }
}