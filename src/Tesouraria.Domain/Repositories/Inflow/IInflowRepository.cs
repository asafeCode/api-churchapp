using Tesouraria.Domain.Dtos.Filters;
using Tesouraria.Domain.Dtos.ReadModels;

namespace Tesouraria.Domain.Repositories.Inflow;

public interface IInflowRepository
{
    Task Add(Entities.Inflow inflow, CancellationToken ct = default);
    Task<Entities.Inflow?> GetById(Guid inflowId, CancellationToken ct = default);
    Task<IEnumerable<InflowDashboardReadModel>> GetAll(InflowFilterDto filter, CancellationToken ct = default);
    void Update(Entities.Inflow inflow);
    Task Delete(Entities.Inflow inflow, CancellationToken ct = default);
}