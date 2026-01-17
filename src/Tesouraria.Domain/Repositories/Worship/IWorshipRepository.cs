namespace Tesouraria.Domain.Repositories.Worship;

public interface IWorshipRepository
{
    Task Add(Entities.ValueObjects.Worship worship, CancellationToken ct = default);
    Task<bool> ExistActiveWorshipWithId(Guid? worshipId,Guid tenantId ,CancellationToken ct = default);
    Task<IEnumerable<Entities.ValueObjects.Worship>> GetAll(Guid tenantId, CancellationToken ct = default);
    Task<IEnumerable<Entities.ValueObjects.Worship>> GetForUpdate(Guid tenantId, CancellationToken ct = default);
}