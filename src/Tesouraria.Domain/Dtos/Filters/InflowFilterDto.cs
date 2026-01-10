using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Domain.Dtos.Filters;

public record InflowFilterDto
{
    public DateOnly? InitialDate { get; init; }
    
    public DateOnly? EndDate { get; init; }
    public InflowType? Type { get; init; }
    public PaymentMethod? PaymentMethod { get; init; }
    public decimal? AmountMin { get; init; }
    public decimal? AmountMax { get; init; }
    public string? Description { get; init; }
    public Guid? CreatedByUserId { get; init; }
    public Guid? MemberId { get; init; }
    public Guid? WorshipId { get; init; }
    
    public InflowOrderBy? OrderBy { get; init; }
    public OrderDirection? OrderDirection { get; init; }
}
