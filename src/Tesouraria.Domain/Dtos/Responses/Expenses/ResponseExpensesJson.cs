using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Domain.Dtos.Responses.Expenses;

public record ResponseExpensesJson
{
   public IEnumerable<ResponseExpenseJson> Expenses { get; init; } = [];
}

public record ResponseExpenseJson
{
   public Guid Id { get; init; }
   public string Name { get; init; } =  string.Empty;
   public ExpenseType ExpenseType { get; init; }
   public int? TotalInstallments { get; init; }
}