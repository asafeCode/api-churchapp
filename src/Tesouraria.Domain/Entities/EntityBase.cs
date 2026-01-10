namespace Tesouraria.Domain.Entities;

public abstract class EntityBase
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public bool Active { get; set; } = true;
    public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;

    public Guid TenantId { get; init; }
}
