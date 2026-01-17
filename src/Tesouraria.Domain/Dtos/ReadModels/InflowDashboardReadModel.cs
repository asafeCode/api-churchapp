using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Domain.Dtos.ReadModels;

public record InflowDashboardReadModel(
    Guid Id,
    string Description,
    DateOnly Date,
    InflowType InflowType,
    string MemberName,
    DayOfWeek? WorshipDay,
    TimeSpan? WorshipTime,
    PaymentMethod PaymentMethod,
    decimal Amount
);

public record InflowsDashboardReadModel(
    IEnumerable<InflowDashboardReadModel> Inflows,
    decimal TotalAmount);