using Tesouraria.Infrastructure.Extensions;
using Tesouraria.Infrastructure.Migrations;

namespace Tesouraria.API.Extensions;

public static class WebApplicationExtension
{
    public static void MigrateDatabase(this WebApplication app, ConfigurationManager configuration)
    {
        var connectionString = configuration.ConnectionString();
        var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        DatabaseMigration.Migrate(connectionString!, serviceScope.ServiceProvider);
    }
}