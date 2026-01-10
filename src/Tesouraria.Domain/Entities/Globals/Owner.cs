namespace Tesouraria.Domain.Entities.Globals;

public sealed class Owner
{
    private Owner() { }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;
    public bool Active { get; private set; } = true;

    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
}

