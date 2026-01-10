using Microsoft.EntityFrameworkCore;
using Tesouraria.Domain.Entities.ValueObjects;
using Tesouraria.Domain.Repositories.Token;

namespace Tesouraria.Infrastructure.DataAccess.Repositories;

public sealed class TokenRepository :  ITokenRepository
{
    private readonly TesourariaDbContext _dbContext;
    public TokenRepository(TesourariaDbContext dbContext) => _dbContext = dbContext;
    
    public async Task<RefreshToken?> GetRefreshToken(string refreshToken) => await _dbContext
        .RefreshTokens
        .AsNoTracking()
        .Include(token => token.User)
        .FirstOrDefaultAsync(token => token.Value.Equals(refreshToken));
    
    public async Task AddRefreshToken(RefreshToken refreshToken)
    { 
        var tokens = _dbContext.RefreshTokens
            .Where(token => token.UserId == refreshToken.UserId);
        
        _dbContext.RefreshTokens.RemoveRange(tokens);
        await _dbContext.RefreshTokens.AddAsync(refreshToken);
    }
}