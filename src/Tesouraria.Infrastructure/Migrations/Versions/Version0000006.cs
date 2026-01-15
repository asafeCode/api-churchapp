using FluentMigrator;

namespace Tesouraria.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.CREATE_INDEX_FOR_FAST_LOGIN, "Create Index for fast login")]
public class Version0000006 : VersionBase
{
    public override void Up()
    {
        Execute.Sql(@"
            CREATE INDEX IF NOT EXISTS IX_Users_NormalizedUsername_TenantId_Active
            ON ""Users"" (""NormalizedUsername"", ""TenantId"")
            WHERE ""Active"" = TRUE;
        ");
    }
}