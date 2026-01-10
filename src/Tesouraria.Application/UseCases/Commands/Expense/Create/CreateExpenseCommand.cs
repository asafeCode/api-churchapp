using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Application.UseCases.Commands.Expense.Create;

public class CreateExpenseCommand : ICommand
{
    public string Name { get; init; } = string.Empty;
    public ExpenseType Type { get; init; }
    public int? TotalInstallments { get; init; }
}