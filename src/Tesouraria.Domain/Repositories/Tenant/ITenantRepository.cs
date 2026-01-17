namespace Tesouraria.Domain.Repositories.Tenant;

public interface ITenantRepository
{
    Task<IEnumerable<Entities.Globals.Tenant>> GetAll(CancellationToken ct =  default);
    Task<bool> ExistTenantWithId(Guid tenantId, CancellationToken ct =  default);
    
}