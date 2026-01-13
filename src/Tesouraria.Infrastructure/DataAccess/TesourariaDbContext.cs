using Microsoft.EntityFrameworkCore;
using Tesouraria.Domain.Entities;
using Tesouraria.Domain.Entities.Globals;
using Tesouraria.Domain.Entities.ValueObjects;
using Tesouraria.Infrastructure.Extensions;

namespace Tesouraria.Infrastructure.DataAccess;

public class TesourariaDbContext : DbContext
{
    public TesourariaDbContext(DbContextOptions options) : base(options) {}
    public DbSet<Owner> Owner { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Inflow> Inflows { get; set; }
    public DbSet<Outflow> Outflows { get; set; }
    public DbSet<Worship> Worships { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<InviteCode> InviteCodes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TesourariaDbContext).Assembly);
    }

}