using FluentMigrator;

namespace Tesouraria.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.ADD_INVITE_CODE, "Create table InviteCode")]
public class Version0000005 : VersionBase
{
    public override void Up()
    {
        CreateTable("InviteCodes")
            .WithColumn("Value").AsString().NotNullable()
            .WithColumn("ExpiresOn").AsDateTimeOffset().NotNullable();
    }
}