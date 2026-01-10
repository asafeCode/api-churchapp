using FluentMigrator;
using Tesouraria.Domain.Entities;
using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Infrastructure.Migrations.Versions;


[Migration(DatabaseVersions.TENANT_TABLE, "Create a table to save the tenants and owner information")]
public class Version0000000 : ForwardOnlyMigration 
{
    public override void Up()
    {
        Create.Table("Owner")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("CreatedOn").AsDateTimeOffset().NotNullable()
            .WithColumn("Active").AsBoolean().NotNullable()
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Email").AsString(255).NotNullable()
            .WithColumn("PasswordHash").AsString(2000).NotNullable();
        
        Insert.IntoTable("Owner").Row(new 
        {
            Id = Guid.NewGuid(),
            CreatedOn = DateTime.UtcNow,
            Active = true,
            Name = "Owner",
            Email = "owner@email.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
        });
        
        Create.Table("Tenants")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("CreatedOn").AsDateTime().NotNullable()
            .WithColumn("Active").AsBoolean().NotNullable()
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("DomainName").AsString(255).Nullable()
            .WithColumn("OwnerId").AsGuid().NotNullable()
            .ForeignKey("FK_Tenants_Owner_Id","Owner", "Id");
    }
}