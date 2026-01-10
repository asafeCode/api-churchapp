using Tesouraria.Domain.Entities.Enums;
using Tesouraria.Domain.Entities.ValueObjects;

namespace Tesouraria.Domain.Entities;

public class Inflow : EntityBase
{
    public DateOnly Date { get; init; }
    public InflowType Type { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
    public decimal Amount { get; init; }
    public string? Description { get; init; }
    public Guid? MemberId { get; init; }
    public User? Member { get; init; } 
    public Guid CreatedByUserId { get; init; }
    public Guid? WorshipId { get; init; }
    public Worship? Worship { get; init; }
    
    
}

