namespace Tesouraria.Domain.Repositories.Owner;

public interface IOwnerRepository
{
    Task<bool> ExistOwnerWithId(Guid ownerId);
    Task<Entities.Globals.Owner?> GetOwnerWithEmail(string email, CancellationToken ct = default);
    Task AddTenant(Entities.Globals.Tenant tenant, CancellationToken ct = default);
}