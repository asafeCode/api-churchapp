namespace Tesouraria.Domain.Entities.Globals;

public class Tenant
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;
    public bool Active { get; private set; } = true;
    public string Name { get; init; } = string.Empty;
    public string? DomainName { get; init; } 
    public Guid OwnerId { get; init; } 
}