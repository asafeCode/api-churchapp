using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Filters;

namespace Tesouraria.Application.UseCases.Queries.OutflowDashboard;

public record OutflowDashboardQuery(OutflowFilterDto Filter) : IQuery;