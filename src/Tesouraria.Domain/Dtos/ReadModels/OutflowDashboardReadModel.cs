using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Domain.Dtos.ReadModels;

public record OutflowDashboardReadModel(
    Guid Id,
    string ExpenseName,
    DateOnly Date,
    decimal Amount,
    PaymentMethod PaymentMethod,
    int? CurrentInstallment,
    int? TotalInstallments
);

public record OutflowsDashboardReadModel(
    IEnumerable<OutflowDashboardReadModel> Outflows,
    decimal TotalAmount);