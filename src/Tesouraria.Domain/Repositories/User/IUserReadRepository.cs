using Tesouraria.Domain.Dtos.Filters;
using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Domain.Repositories.User;

public interface IUserReadRepository
{
    public Task<bool> ExistActiveUserWithId(Guid? userId, CancellationToken ct = default);
    public Task<bool> ExistTenantFromUserId(Guid userId, Guid tenantId, CancellationToken ct = default);
    public Task<Entities.User?> GetActiveUserById(Guid userId, Guid tenantId, CancellationToken ct = default);
    public Task<IEnumerable<Entities.User>?> GetAll(UserFilterDto filters, Guid tenantId, CancellationToken ct = default);
    public Task<UserRole?> GetActiveUserRoleById(Guid userId, Guid tenantId, CancellationToken ct = default);
    public Task<bool> ExistsUserWithName(string name, Guid tenantId, CancellationToken ct);
    public Task<Entities.User?> GetUserByName(string name, Guid tenantId, CancellationToken ct = default);
}