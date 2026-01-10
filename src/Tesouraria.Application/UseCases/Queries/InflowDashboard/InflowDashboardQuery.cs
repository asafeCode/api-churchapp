using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Filters;

namespace Tesouraria.Application.UseCases.Queries.InflowDashboard;

public record InflowDashboardQuery(InflowFilterDto Filter) : IQuery;