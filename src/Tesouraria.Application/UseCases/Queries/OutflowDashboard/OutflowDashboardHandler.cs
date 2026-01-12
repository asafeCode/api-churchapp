using Tesouraria.Application.Mappers;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Outflows;
using Tesouraria.Domain.Repositories.Outflow;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Queries.OutflowDashboard;

public class OutflowDashboardHandler : IQueryHandler<OutflowDashboardQuery, ResponseOutflowsJson>
{
    private readonly ILoggedUser _loggedUser;
    private readonly IOutflowRepository  _outflowRepository;

    public OutflowDashboardHandler(IOutflowRepository outflowRepository, ILoggedUser loggedUser)
    {
        _outflowRepository = outflowRepository;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseOutflowsJson> HandleAsync(OutflowDashboardQuery query, CancellationToken ct = default)
    {
        var (_, tenantId) = _loggedUser.User();
        var response = await _outflowRepository.GetAll(query.Filter, tenantId, ct);

        return new ResponseOutflowsJson
        {
            Outflows = response.Outflows.ToResponse(),
            TotalAmount = response.TotalAmount
        };
    }
}