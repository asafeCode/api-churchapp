using System.Linq.Expressions;
using Tesouraria.Domain.Dtos.Filters;
using Tesouraria.Domain.Dtos.ReadModels;
using Tesouraria.Domain.Dtos.Responses.Outflows;

namespace Tesouraria.Domain.Repositories.Outflow;

public interface IOutflowRepository
{
    Task Add(Entities.Outflow outflow, CancellationToken ct = default);
    Task<Entities.Outflow?> GetById(Guid outflowId, CancellationToken ct = default);
    Task<IEnumerable<OutflowDashboardReadModel>> GetAll(OutflowFilterDto filter, CancellationToken ct = default);
    void Update(Entities.Outflow outflow);
    Task Delete(Entities.Outflow outflow, CancellationToken ct = default);
}