using Tesouraria.Application.Mappers;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Users;
using Tesouraria.Domain.Exceptions.ExceptionsBase;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Logged;

namespace Tesouraria.Application.UseCases.Queries.GetUserById;

public class GetUserByIdHandler : IQueryHandler<GetUserByIdQuery, ResponseUserJson>
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserReadRepository _readRepository;

    public GetUserByIdHandler(ILoggedUser loggedUser, IUserReadRepository readRepository)
    {
        _loggedUser = loggedUser;
        _readRepository = readRepository;
    }
    public async Task<ResponseUserJson> HandleAsync(GetUserByIdQuery query, CancellationToken ct = default)
    {
        var (_, tenantId) =  _loggedUser.User();
        var user = await _readRepository.GetActiveUserById(query.UserId, tenantId, ct);
        return user is null ? throw new NotFoundException("Usuário não encontrado") : user.ToResponse();
    }
}