using Microsoft.EntityFrameworkCore;
using Tesouraria.Domain.Dtos.Filters;
using Tesouraria.Domain.Entities;
using Tesouraria.Domain.Entities.Enums;
using Tesouraria.Domain.Entities.Helpers;
using Tesouraria.Domain.Extensions;
using Tesouraria.Domain.Repositories.User;

namespace  Tesouraria.Infrastructure.DataAccess.Repositories;

public sealed class UserRepository : IUserReadRepository, IUserUpdateRepository, IUserWriteRepository
{
    private readonly TesourariaDbContext _dbContext;
    public UserRepository(TesourariaDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<User>?> GetAll(UserFilterDto filter, Guid tenantId, CancellationToken ct = default)
    {
        var query = _dbContext
            .Users
            .AsNoTracking()
            .Where(u => u.Active && u.TenantId == tenantId);

        if (filter.Name.IsNotEmpty())
            query = query.Where(u =>
                u.NormalizedUsername.Contains(filter.Name.NormalizeUsername()));

        if (filter.Role.HasValue)
            query = query.Where(u => u.Role == filter.Role.Value);

        if (filter.DateOfBirthInicial.HasValue)
            query = query.Where(u =>
                u.DateOfBirth >= filter.DateOfBirthInicial.Value);

        if (filter.DateOfBirthFinal.HasValue)
            query = query.Where(u =>
                u.DateOfBirth <= filter.DateOfBirthFinal.Value);

        return await query.ToListAsync(ct);
    }
    
    public async Task<UserRole?> GetActiveUserRoleById(Guid userId, Guid tenantId, CancellationToken ct = default) =>
        await _dbContext.Users
            .AsNoTracking()
            .Where(u => u.Active && u.Id == userId && u.TenantId == tenantId)
            .Select(u => u.Role) 
            .FirstOrDefaultAsync(ct);

    public async Task<bool> ExistsUserWithName(string name, Guid tenantId, CancellationToken ct) => await _dbContext
        .Users
        .AnyAsync(user => user.Username.Equals(name) && user.TenantId == tenantId, ct);

    public async Task<User?> GetUserByName(string name, Guid tenantId, CancellationToken ct) => await _dbContext
        .Users
        .AsNoTracking()
        .FirstOrDefaultAsync(user => user.NormalizedUsername.Equals(name) && user.Active && user.TenantId == tenantId, ct);
    
    public async Task<bool> ExistActiveUserWithId(Guid? userId, CancellationToken ct = default) => await _dbContext
        .Users
        .AnyAsync(user => user.Id.Equals(userId) && user.Active, ct);

    public async Task<bool> ExistTenantFromUserId(Guid userId, Guid tenantId, CancellationToken ct = default) => await _dbContext
        .Users
        .AsNoTracking()
        .AnyAsync(user => user.Id.Equals(userId) && user.Active && user.TenantId.Equals(tenantId), ct);

    public async Task<User?> GetActiveUserById(Guid userId, Guid tenantId, CancellationToken ct = default) =>
        await _dbContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Active && user.Id == userId && user.TenantId == tenantId, ct);
    
    public async Task AddUserAsync(User user) => await _dbContext.Users.AddAsync(user);
    
    public async Task DeleteAccount(Guid userId, CancellationToken ct)
    {
        var user = await _dbContext.Users.FirstAsync(u => u.Id == userId, ct);
        user.Active = false;
        
        var inflows = await _dbContext.Inflows.Where(i => i.CreatedByUserId == userId).ToListAsync(ct);
        inflows.ForEach(i => i.Active = false);

        var outflows = await _dbContext.Outflows.Where(o => o.CreatedByUserId == userId).ToListAsync(ct);
        outflows.ForEach(o => o.Active = false);
        
        var refreshTokens = await _dbContext.RefreshTokens.Where(t => t.UserId == userId).ToListAsync(ct);
        _dbContext.RefreshTokens.RemoveRange(refreshTokens);
    }
    public async Task<User> GetUserById(Guid id, Guid tenantId, CancellationToken ct) => await _dbContext
        .Users
        .FirstAsync(user => user.Id.Equals(id) && user.Active && user.TenantId == tenantId, ct);
    public void Update(User user) => _dbContext.Users.Update(user);
}