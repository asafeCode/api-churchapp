using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Application.UseCases.Commands.Outflow.Create;

public record CreateOutflowCommand : ICommand
{
    public DateOnly Date { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
    public decimal? Amount { get; init; }
    public string? Description { get; init; }
    public Guid ExpenseId { get; init; }
}