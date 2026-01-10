using Tesouraria.Application.Mappers;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Outflows;
using Tesouraria.Domain.Repositories.Outflow;

namespace Tesouraria.Application.UseCases.Queries.OutflowDashboard;

public class OutflowDashboardHandler : IQueryHandler<OutflowDashboardQuery, ResponseOutflowsJson>
{
    private readonly IOutflowRepository  _outflowRepository;

    public OutflowDashboardHandler(IOutflowRepository outflowRepository) => _outflowRepository = outflowRepository;
    public async Task<ResponseOutflowsJson> HandleAsync(OutflowDashboardQuery query, CancellationToken ct = default)
    {
        var outflows = await _outflowRepository.GetAll(query.Filter, ct);
        
        return outflows.ToResponse();
    }
}