using Tesouraria.Domain.Entities.Enums;
using Tesouraria.Domain.Entities.ValueObjects;

namespace Tesouraria.Domain.Entities;

public class Outflow : EntityBase
{
    public DateOnly Date { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
    public decimal Amount { get; init; }
    
    public int? CurrentInstallmentPayed {get; set;}
    public string? Description { get; init; }
    public Guid ExpenseId { get; init; }
    public Expense Expense { get; init; } = null!;
    public Guid CreatedByUserId { get; init; }
}