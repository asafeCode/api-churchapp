using Microsoft.AspNetCore.Mvc;
using Tesouraria.API.Filters;

namespace Tesouraria.API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public string[] Roles { get; }
    public AuthenticatedUserAttribute(params string[] roles) : base(typeof(AuthenticatedUserFilter))
    {
        Roles = roles;
    }
}