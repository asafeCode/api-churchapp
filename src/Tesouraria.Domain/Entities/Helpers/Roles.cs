using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Domain.Entities.Helpers;

public static class Roles
{
    public const string Admin = nameof(UserRole.Admin);
    public const string Member = nameof(UserRole.Member);
    public const string Visitor = nameof(UserRole.Visitor);
}