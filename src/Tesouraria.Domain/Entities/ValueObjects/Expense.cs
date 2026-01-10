using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Domain.Entities.ValueObjects;

public class Expense : EntityBase
{
    public string Name { get; init; } = string.Empty;
    public ExpenseType Type { get; init; }
    public int? TotalInstallments { get; init; }
}