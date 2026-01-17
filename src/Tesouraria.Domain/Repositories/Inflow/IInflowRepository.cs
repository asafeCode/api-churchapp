using Tesouraria.Domain.Dtos.Filters;
using Tesouraria.Domain.Dtos.ReadModels;

namespace Tesouraria.Domain.Repositories.Inflow;

public interface IInflowRepository
{
    Task Add(Entities.Inflow inflow, CancellationToken ct = default);
    Task<Entities.Inflow?> GetForUpdate(Guid inflowId, Guid tenantId, CancellationToken ct = default);
    Task<InflowsDashboardReadModel> GetAll(InflowFilterDto filter, Guid tenantId ,CancellationToken ct = default);
}