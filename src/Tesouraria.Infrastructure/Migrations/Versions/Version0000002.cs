using FluentMigrator;

namespace Tesouraria.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.EXPENSE_WORSHIP_TABLE, "Create a table to save the expenses and worships information")]
public class Version0000002 : VersionBase
{
    public override void Up()
    {
        CreateTable("Expenses")
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Type").AsInt32().NotNullable()
            .WithColumn("CurrentInstallment").AsInt32().Nullable()
            .WithColumn("TotalInstallments").AsInt32().Nullable()
            .WithColumn("AmountOfEachInstallment").AsDecimal(18,2).Nullable();      
        
        CreateTable("Worships")
            .WithColumn("DayOfWeek").AsInt32().NotNullable()
            .WithColumn("Time").AsTime().NotNullable()
            .WithColumn("Description").AsString(255).Nullable();
    }
}