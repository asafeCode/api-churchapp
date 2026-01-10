using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Filters;

namespace Tesouraria.Application.UseCases.Queries.UsersDashboard;

public record UserDashboardQuery(UserFilterDto Filters) : IQuery;