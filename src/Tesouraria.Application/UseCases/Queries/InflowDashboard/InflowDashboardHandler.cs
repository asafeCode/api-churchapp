using Tesouraria.Application.Mappers;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Inflows;
using Tesouraria.Domain.Repositories.Inflow;

namespace Tesouraria.Application.UseCases.Queries.InflowDashboard;

public class InflowDashboardHandler : IQueryHandler<InflowDashboardQuery, ResponseInflowsJson>
{
    private readonly IInflowRepository  _inflowRepository;

    public InflowDashboardHandler(IInflowRepository inflowRepository) => _inflowRepository = inflowRepository;
    public async Task<ResponseInflowsJson> HandleAsync(InflowDashboardQuery query, CancellationToken ct = default)
    {
        var inflows = await _inflowRepository.GetAll(query.Filter, ct);
        
        return inflows.ToResponse();
    }
}