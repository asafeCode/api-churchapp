using Microsoft.AspNetCore.Mvc;
using Tesouraria.API.Filters;

namespace Tesouraria.API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class AuthenticatedOwnerAttribute : TypeFilterAttribute
{
    public AuthenticatedOwnerAttribute() : base(typeof(AuthenticatedOwnerFilter)){}
}