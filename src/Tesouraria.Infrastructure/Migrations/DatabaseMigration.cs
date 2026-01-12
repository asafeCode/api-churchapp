using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Tesouraria.Infrastructure.Migrations;

public static class DatabaseMigration
{
    public static void Migrate(string connectionString, IServiceProvider serviceProvider)
    {
        EnsureDatabaseCreated_Postgres(connectionString);
        MigrationDatabase(serviceProvider);
    }

    private static void EnsureDatabaseCreated_Postgres(string connectionString)
    {
        var builder = new NpgsqlConnectionStringBuilder(connectionString);
        var databaseName = builder.Database;

        builder.Database = "postgres";

        using var connection = new NpgsqlConnection(builder.ConnectionString);
        connection.Open();

        var exists = connection.ExecuteScalar<bool>(
            "SELECT EXISTS (SELECT 1 FROM pg_database WHERE datname = @name)",
            new { name = databaseName });

        if (!exists)
        {
            connection.Execute($@"CREATE DATABASE ""{databaseName}""");
        }
    }

    private static void MigrationDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.ListMigrations();
        runner.MigrateUp();
    }
}