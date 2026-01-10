using FluentMigrator;

namespace Tesouraria.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.USER_TABLE, "Create a table to save the user's information")]
public class Version0000001 : VersionBase
{
    public override void Up()
    {
        CreateTable("Users")
            .WithColumn("Username").AsString(255).NotNullable()
            .WithColumn("NormalizedUsername").AsString(255).NotNullable()
            .WithColumn("PasswordHash").AsString(2000).NotNullable()
            .WithColumn("Role").AsInt32().NotNullable()
            .WithColumn("DateOfBirth").AsDate().NotNullable()

            .WithColumn("FullName").AsString(255).Nullable()
            .WithColumn("Gender").AsInt32().Nullable()
            .WithColumn("Phone").AsString(255).Nullable()
            .WithColumn("ProfessionalWork").AsString(255).Nullable()

            .WithColumn("EntryDate").AsDate().Nullable()
            .WithColumn("ConversionDate").AsDate().Nullable()
            .WithColumn("IsBaptized").AsBoolean().Nullable()

            .WithColumn("City").AsString(255).Nullable()
            .WithColumn("Neighborhood").AsString(255).Nullable();
    }
}