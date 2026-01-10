namespace Tesouraria.Domain.Repositories.User;

public interface IUserUpdateRepository
{
    public Task<Entities.User> GetUserById(Guid id, Guid tenantId, CancellationToken ct);
    public void Update(Entities.User user);
}