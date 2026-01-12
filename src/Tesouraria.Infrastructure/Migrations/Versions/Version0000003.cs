using FluentMigrator;

namespace Tesouraria.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.IN_OUT_FLOWS_TABLE, "Create table to save the inflows and outflows.")]
public class Version0000003 : VersionBase
{
    public override void Up()
    {
        CreateTable("Inflows")
            .WithColumn("Date").AsDate().NotNullable()
            .WithColumn("Type").AsInt32().NotNullable()
            .WithColumn("PaymentMethod").AsInt32().NotNullable()
            .WithColumn("Amount").AsDecimal(18, 2).NotNullable()
            .WithColumn("Description").AsString(255).Nullable()

            .WithColumn("MemberId").AsGuid().Nullable()
            .ForeignKey("FK_Inflows_Users_Member_Id", "Users", "Id")
            
            .WithColumn("CreatedByUserId").AsGuid().NotNullable()
            .ForeignKey("FK_Inflows_Users_CreatedByUser_Id", "Users", "Id")

            .WithColumn("WorshipId").AsGuid().Nullable()
            .ForeignKey("FK_Inflows_Worship_Id", "Worships", "Id");

        CreateTable("Outflows")
            .WithColumn("Date").AsDate().NotNullable()
            .WithColumn("PaymentMethod").AsInt32().NotNullable()
            .WithColumn("Amount").AsDecimal(18, 2).NotNullable()
            .WithColumn("CurrentInstallmentPayed").AsInt32().Nullable()
            .WithColumn("Description").AsString(255).Nullable()

            .WithColumn("ExpenseId").AsGuid().NotNullable()
            .ForeignKey("FK_Outflows_Expense_Id", "Expenses", "Id")

            .WithColumn("CreatedByUserId").AsGuid().NotNullable()
            .ForeignKey("FK_Outflows_Users_CreatedByUserId", "Users", "Id");

    }
}