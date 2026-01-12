namespace Tesouraria.Domain.Repositories.Worship;

public interface IWorshipRepository
{
    Task Add(Entities.ValueObjects.Worship worship, CancellationToken ct = default);
    Task<bool> ExistActiveWorshipWithId(Guid? worshipId,Guid tenantId ,CancellationToken ct = default);
    Task<IEnumerable<Entities.ValueObjects.Worship>> GetAll(Guid tenantId, CancellationToken ct = default);
    void Update(Entities.ValueObjects.Worship worship, CancellationToken ct = default);
    
    // Não é possível deletar o culto, se existir Entradas associadas.
    Task Delete(Entities.ValueObjects.Worship worship, Guid tenantId, CancellationToken ct = default);
    
    
}