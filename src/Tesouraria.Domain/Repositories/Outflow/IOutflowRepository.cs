using Tesouraria.Domain.Dtos.Filters;
using Tesouraria.Domain.Dtos.ReadModels;

namespace Tesouraria.Domain.Repositories.Outflow;

public interface IOutflowRepository
{
    Task Add(Entities.Outflow outflow, CancellationToken ct = default);
    Task<Entities.Outflow?> GetForUpdate(Guid outflowId, Guid tenantId ,CancellationToken ct = default);
    Task<OutflowsDashboardReadModel> GetAll(OutflowFilterDto filter, Guid tenantId, CancellationToken ct = default);
}