using Tesouraria.Application.Mappers;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Users;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Queries.UsersDashboard;

public class UserDashboardHandler : IQueryHandler<UserDashboardQuery, ResponseUsersJson>
{
    private readonly IUserReadRepository _readRepository;
    private readonly ILoggedUser _loggedUser;

    public UserDashboardHandler(IUserReadRepository readRepository, ILoggedUser loggedUser)
    {
        _readRepository = readRepository;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseUsersJson> HandleAsync(UserDashboardQuery query, CancellationToken ct = default)
    {
        var (_, tenantId) = _loggedUser.User();
        var users = await  _readRepository.GetAll(query.Filters, tenantId, ct);

        return users.ToResponse();
    }
}