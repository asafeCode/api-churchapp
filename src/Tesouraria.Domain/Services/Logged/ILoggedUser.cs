using Tesouraria.Domain.Entities;

namespace Tesouraria.Domain.Services.Logged;

public interface ILoggedUser
{
    public (Guid userId, Guid tenantId) User();
}