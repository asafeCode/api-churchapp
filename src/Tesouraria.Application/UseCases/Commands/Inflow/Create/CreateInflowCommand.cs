using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Application.UseCases.Commands.Inflow.Create;

public class CreateInflowCommand : ICommand
{
    public DateOnly Date { get; init; }
    public InflowType Type { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
    public decimal Amount { get; init; }
    public string? Description { get; init; }
    public Guid? WorshipId { get; init; }
    public Guid? UserId { get; init; }
}