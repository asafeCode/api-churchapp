using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace Tesouraria.Infrastructure.Migrations.Versions;

public abstract class VersionBase : ForwardOnlyMigration
{
    protected ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string table)
    {
        var tableSyntax = Create.Table(table)
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("CreatedOn").AsDateTimeOffset().NotNullable()
            .WithColumn("Active").AsBoolean().NotNullable()
            .WithColumn("TenantId").AsGuid().NotNullable()
            .ForeignKey($"FK_{table}_Tenant_Id", "Tenants", "Id");
        
        return tableSyntax;
    }
}