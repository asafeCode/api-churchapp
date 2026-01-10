using Tesouraria.Domain.Entities;
using Tesouraria.Domain.Entities.Globals;

namespace Tesouraria.Domain.Services.Logged;

public interface ILoggedOwner
{
    public Guid Owner();
}