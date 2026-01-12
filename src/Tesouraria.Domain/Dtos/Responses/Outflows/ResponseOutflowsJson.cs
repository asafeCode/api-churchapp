using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Domain.Dtos.Responses.Outflows;

public record ResponseOutflowsJson
{
    public IEnumerable<ResponseShortOutflowJson> Outflows { get; set; } = [];
    public decimal TotalAmount { get; set; }
}

public record ResponseShortOutflowJson(
    Guid Id,
    string ExpenseName,
    DateOnly Date,
    decimal Amount,
    PaymentMethod PaymentMethod,
    int? CurrentInstallment,
    int? TotalInstallments
    );