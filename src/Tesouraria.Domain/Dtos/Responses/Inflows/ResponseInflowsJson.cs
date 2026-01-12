using Tesouraria.Domain.Entities;
using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Domain.Dtos.Responses.Inflows;

public record ResponseInflowsJson
{
    public IEnumerable<ResponseShortInflowJson> Inflows { get; set; } = [];
    public decimal TotalAmount { get; set; }
}

public record ResponseShortInflowJson(
    Guid Id,
    Guid WorshipId,
    Guid MemberId,
    DateOnly Date,
    InflowType InflowType,
    string MemberName,
    string WorshipInfo,
    PaymentMethod PaymentMethod,
    decimal Amount
    );