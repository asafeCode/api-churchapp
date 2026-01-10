namespace Tesouraria.Domain.Repositories.User;

public interface IUserWriteRepository
{
    public Task AddUserAsync(Entities.User user);
    public Task DeleteAccount(Guid userId, CancellationToken ct = default);
}