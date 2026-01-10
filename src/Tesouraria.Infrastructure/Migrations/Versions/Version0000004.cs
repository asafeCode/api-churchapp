using FluentMigrator;

namespace Tesouraria.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.REFRESH_TOKEN_TABLE, "Create table to save the refresh token")]
    public class Version0000004 : VersionBase
    {
        public override void Up()
        {
            CreateTable("RefreshTokens")
                .WithColumn("Value").AsString().NotNullable()
                .WithColumn("ExpiresOn").AsDateTimeOffset().NotNullable()
                .WithColumn("UserId").AsGuid().NotNullable().ForeignKey("FK_RefreshTokens_User_Id", "Users", "Id");
        }
    }
