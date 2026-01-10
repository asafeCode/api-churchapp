using Tesouraria.Application.Mappers;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Users;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Queries.UserProfile;

public record GetUserProfileQuery : IQuery;
public class UserProfileHandler : IQueryHandler<GetUserProfileQuery, ResponseUserJson>
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserReadRepository _readRepository;
    
    public UserProfileHandler(ILoggedUser loggedUser, IUserReadRepository readRepository)
    {
        _loggedUser = loggedUser;
        _readRepository = readRepository;
    }

    public async Task<ResponseUserJson> HandleAsync(GetUserProfileQuery query, CancellationToken ct = default)
    {
        var (loggedUserId, tenantId) =  _loggedUser.User();
        var user = await _readRepository.GetActiveUserById(loggedUserId, tenantId, ct);
        return user!.ToResponse();
    }
}