using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Domain.Dtos.ReadModels;

public record InflowDashboardReadModel(
    Guid Id,
    Guid? WorshipId,
    Guid? MemberId,
    DateOnly Date,
    InflowType InflowType,
    string MemberName,
    DayOfWeek? WorshipDay,
    TimeSpan? WorshipTime,
    PaymentMethod PaymentMethod,
    decimal Amount
);