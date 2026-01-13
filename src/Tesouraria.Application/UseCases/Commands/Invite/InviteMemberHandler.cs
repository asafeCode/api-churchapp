using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Repositories.Token;
using Tesouraria.Domain.Services.Link;
using Tesouraria.Domain.Services.Logged;
using Tesouraria.Domain.Services.Token;

namespace Tesouraria.Application.UseCases.Commands.Invite;

public record CreateInviteCommand() : ICommand;

public class InviteMemberHandler : ICommandHandler<CreateInviteCommand, CreateInviteResponseDto>
{
    private readonly ILoggedUser _loggedUser;
    private readonly IInviteCodeGenerator  _inviteCodeGenerator;
    private readonly IInviteLinkGenerator  _inviteLinkGenerator;
    private readonly ITokenRepository  _tokenRepository;

    public InviteMemberHandler(ILoggedUser loggedUser, 
        IInviteCodeGenerator inviteCodeGenerator, 
        IInviteLinkGenerator inviteLinkGenerator, 
        ITokenRepository tokenRepository)
    {
        _loggedUser = loggedUser;
        _inviteCodeGenerator = inviteCodeGenerator;
        _inviteLinkGenerator = inviteLinkGenerator;
        _tokenRepository = tokenRepository;
    }
    public async Task<CreateInviteResponseDto> HandleAsync(CreateInviteCommand request, CancellationToken ct = default)
    {
        var (_, tenantId) = _loggedUser.User();
        var inviteCode = _inviteCodeGenerator.CreateCode(tenantId);
        var link = _inviteLinkGenerator.GetLink(inviteCode);
        
        await _tokenRepository.AddInviteCode(inviteCode, ct);
        return new CreateInviteResponseDto(link);
    }
}

public record CreateInviteResponseDto(string Link);