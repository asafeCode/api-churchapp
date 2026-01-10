namespace Tesouraria.Domain.Repositories.Tenant;

public interface ITenantRepository
{
    Task<IEnumerable<Entities.Globals.Tenant>> GetAll(CancellationToken ct =  default);
}