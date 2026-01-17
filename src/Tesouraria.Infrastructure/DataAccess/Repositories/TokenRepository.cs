using Microsoft.EntityFrameworkCore;
using Tesouraria.Domain.Dtos.ReadModels;
using Tesouraria.Domain.Entities.Globals;
using Tesouraria.Domain.Entities.ValueObjects;
using Tesouraria.Domain.Repositories.Token;

namespace Tesouraria.Infrastructure.DataAccess.Repositories;

public sealed class TokenRepository :  ITokenRepository
{
    private readonly TesourariaDbContext _dbContext;
    public TokenRepository(TesourariaDbContext dbContext) => _dbContext = dbContext;

    public async Task<RefreshTokenReadModel?> GetRefreshTokenForUpdate(string refreshToken, CancellationToken ct = default) =>
        await _dbContext
            .RefreshTokens
            .AsNoTracking()
            .Where(r => r.Value == refreshToken)
            .Select(r => new RefreshTokenReadModel
            {
                UserId = r.User.Id,
                TenantId = r.User.TenantId,
                UserRole = r.User.Role,
                ExpiresOn = r.ExpiresOn,
            })
            .FirstOrDefaultAsync(ct); 
    
    public async Task AddRefreshTokenSafe(RefreshToken refreshToken, CancellationToken ct = default)
    { 
        await _dbContext.RefreshTokens
            .Where(token => token.UserId == refreshToken.UserId)
            .ExecuteDeleteAsync(ct);
        
        await _dbContext.RefreshTokens.AddAsync(refreshToken, ct);
    }

    public async Task<InviteCodeReadModel?> GetInviteCode(string code, CancellationToken ct = default) =>
        await _dbContext
            .InviteCodes
            .AsNoTracking()
            .Where(i => i.Value == code)
            .Select(i => new InviteCodeReadModel
            {
                ExpiresOn = i.ExpiresOn,
                TenantId = i.TenantId,
                Value = i.Value,
                TenantName = i.Tenant.Name
            }).FirstOrDefaultAsync(ct);

    public async Task AddInviteCode(InviteCode inviteCode, CancellationToken ct = default)
    {
        await _dbContext.InviteCodes
            .Where(code => 
                code.TenantId == inviteCode.TenantId)
            .ExecuteDeleteAsync(ct);
        
        await _dbContext.InviteCodes.AddAsync(inviteCode, ct);
    }
}