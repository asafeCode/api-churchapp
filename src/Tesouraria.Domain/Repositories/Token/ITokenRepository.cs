using Tesouraria.Domain.Dtos.ReadModels;
using Tesouraria.Domain.Entities.ValueObjects;

namespace Tesouraria.Domain.Repositories.Token;

public interface ITokenRepository
{
    Task<RefreshTokenReadModel?> GetRefreshTokenForUpdate(string refreshToken, CancellationToken ct = default);
    Task AddRefreshTokenSafe(RefreshToken refreshToken, CancellationToken ct = default); 
    Task<Entities.Globals.Tenant?> GetTenantByInviteCode(InviteCode inviteCode, CancellationToken ct = default);
    Task<InviteCodeReadModel?> GetInviteCode(string code, CancellationToken ct = default);
    Task AddInviteCode(InviteCode inviteCode, CancellationToken ct = default);
}