using Microsoft.AspNetCore.Mvc;
using Tesouraria.API.Attributes;
using Tesouraria.API.Controllers.Base;
using Tesouraria.Application.UseCases.Commands.Invite;
using Tesouraria.Application.UseCases.Queries.VerifyInviteCode;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Entities.Helpers;

namespace Tesouraria.API.Controllers.App;

public class InvitesController : TesourariaControllerBase
{
    [AuthenticatedUser(Roles.Admin)]
    [HttpPost]
    [ProducesResponseType(typeof(CreateInviteResponseDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> InviteMember([FromServices] IMediator mediator)
    {
        var command = new CreateInviteCommand();
        var response = await mediator.SendAsync<CreateInviteCommand, CreateInviteResponseDto>(command);
        return Created(string.Empty, response);
    }

    [HttpGet]
    [Route("verify")]
    [ProducesResponseType(typeof(VerifyInviteCodeResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> VerifyInviteCode([FromServices] IMediator mediator,
        [FromQuery] VerifyInviteCodeQuery query)
    {
        var response = await mediator.QueryAsync<VerifyInviteCodeQuery, VerifyInviteCodeResponseDto>(query);
        return Ok(response);
    }
    
}