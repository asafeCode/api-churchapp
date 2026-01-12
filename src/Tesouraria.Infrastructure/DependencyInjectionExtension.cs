using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Repositories;
using Tesouraria.Domain.Repositories.Expense;
using Tesouraria.Domain.Repositories.Inflow;
using Tesouraria.Domain.Repositories.Outflow;
using Tesouraria.Domain.Repositories.Owner;
using Tesouraria.Domain.Repositories.Report;
using Tesouraria.Domain.Repositories.Tenant;
using Tesouraria.Domain.Repositories.Token;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Repositories.Worship;
using Tesouraria.Domain.Services.Balance;
using Tesouraria.Domain.Services.Logged;
using Tesouraria.Domain.Services.Security;
using Tesouraria.Domain.Services.Token;
using Tesouraria.Infrastructure.DataAccess;
using Tesouraria.Infrastructure.DataAccess.Repositories;
using Tesouraria.Infrastructure.Extensions;
using Tesouraria.Infrastructure.Services.Logged;
using Tesouraria.Infrastructure.Services.Mediator;
using Tesouraria.Infrastructure.Services.Security;
using Tesouraria.Infrastructure.Services.Token;
using Tesouraria.Infrastructure.Settings;

namespace Tesouraria.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext_Postgres(services, configuration);
        AddFluentMigrator_Postgres(services, configuration);
        AddRepositories(services);
        AddTokens(services, configuration);
        AddLoggedUser(services);
        AddPasswordEncripter(services);
    }
    private static void AddDbContext_Postgres(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();
        services.AddDbContext<TesourariaDbContext>(dbOpt => 
            dbOpt.UseNpgsql(connectionString));
    }  
    private static void AddFluentMigrator_Postgres(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();
        
        services.AddFluentMigratorCore().ConfigureRunner(options => 
            options
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.Load("Tesouraria.Infrastructure")).For.All()
        );
    }
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IOwnerRepository, OwnerRepository>();
        services.AddScoped<ITenantRepository, TenantRepository>();
        
        services.AddScoped<IUserWriteRepository, UserRepository>();
        services.AddScoped<IUserUpdateRepository, UserRepository>();
        services.AddScoped<IUserReadRepository, UserRepository>();
        
        services.AddScoped<IInflowRepository, InflowRepository>();
        services.AddScoped<IOutflowRepository, OutflowRepository>();
        services.AddScoped<IWorshipRepository, WorshipRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<ICurrentBalance, BalanceRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IMediator, Mediator>();
        services.AddScoped<ITokenRepository, TokenRepository>();
    }
    private static void AddLoggedUser(IServiceCollection services)
    { 
        services.AddScoped<ILoggedUser, LoggedUser>();
        services.AddScoped<ILoggedOwner, LoggedOwner>();
    }
    private static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(options =>
            configuration.GetSection("Settings:Tokens:Jwt").Bind(options));
        services.AddScoped<IAccessTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IAccessTokenValidator, JwtTokenValidator>();
        
        services.Configure<RefreshTokenSettings>(options =>
            configuration.GetSection("Settings:Tokens:Refresh").Bind(options));
        services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
    }
    private static void AddPasswordEncripter(this IServiceCollection services)
    {
        services.AddScoped<IPasswordEncripter, BcryptEncripter>();
    }
}