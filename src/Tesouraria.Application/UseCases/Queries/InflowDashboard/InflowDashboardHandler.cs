using Tesouraria.Application.Mappers;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Inflows;
using Tesouraria.Domain.Repositories.Inflow;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Queries.InflowDashboard;

public class InflowDashboardHandler : IQueryHandler<InflowDashboardQuery, ResponseInflowsJson>
{
    private readonly IInflowRepository  _inflowRepository;
    private readonly ILoggedUser _loggedUser;

    public InflowDashboardHandler(IInflowRepository inflowRepository, ILoggedUser loggedUser)
    {
        _inflowRepository = inflowRepository;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseInflowsJson> HandleAsync(InflowDashboardQuery query, CancellationToken ct = default)
    {
        var (_, tenantId) = _loggedUser.User();
        var response = await _inflowRepository.GetAll(query.Filter, tenantId, ct);

        return new ResponseInflowsJson
        {
            Inflows = response.Inflows.ToResponse(),
            TotalAmount = response.TotalAmount
        };
    }
}