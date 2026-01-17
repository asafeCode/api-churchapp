using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Domain.Dtos.Filters;

public record OutflowFilterDto
{
    public DateOnly? InitialDate { get; init; }
    public DateOnly? EndDate { get; init; }
    public PaymentMethod? PaymentMethod { get; init; }
    public decimal? AmountMin { get; init; }
    public decimal? AmountMax { get; init; }
    public string? Description { get; init; }
    public ExpenseType? ExpenseType { get; init; }
    public Guid? ExpenseId { get; init; }
    public Guid? CreatedByUserId { get; init; }
}
