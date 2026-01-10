using Tesouraria.Application.Mappers;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Tenants;
using Tesouraria.Domain.Repositories.Tenant;

namespace Tesouraria.Application.UseCases.Queries.TenantsDashboard;

public record GetTenantsDashboardQuery() : IQuery;
public class GetTenantsDashboardHandler : IQueryHandler<GetTenantsDashboardQuery, ResponseTenantsJson>
{
    private readonly ITenantRepository _tenantRepository;
    public GetTenantsDashboardHandler(ITenantRepository tenantRepository) => _tenantRepository = tenantRepository;
    public async Task<ResponseTenantsJson> HandleAsync(GetTenantsDashboardQuery request, CancellationToken ct = default)
    {
        var tenants = await _tenantRepository.GetAll(ct);
        return tenants.ToResponse();
    }
}