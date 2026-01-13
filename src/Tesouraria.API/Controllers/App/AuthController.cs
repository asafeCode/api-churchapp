using Microsoft.AspNetCore.Mvc;
using Tesouraria.API.Attributes;
using Tesouraria.API.Controllers.Base;
using Tesouraria.Application.UseCases.Commands.Auth.Login.Owner;
using Tesouraria.Application.UseCases.Commands.Auth.Login.User;
using Tesouraria.Application.UseCases.Commands.Auth.Register.Member;
using Tesouraria.Application.UseCases.Commands.Auth.Register.Users;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses;
using Tesouraria.Domain.Dtos.Responses.Users;
using Tesouraria.Domain.Entities.Helpers;

namespace Tesouraria.API.Controllers.App;

[Route("auth/login")]
public class AuthController : TesourariaControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseLoggedUserJson),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromServices] IMediator handler, [FromBody] DoLoginCommand command)
    {
        var result = await handler.SendAsync<DoLoginCommand, ResponseLoggedUserJson>(command);
        return Ok(result);
    }
    
    [HttpPost]
    [Route("owner")]
    [ProducesResponseType(typeof(ResponseLoggedOwnerJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> OwnerLogin([FromServices] IMediator mediator,
        [FromBody] OwnerLoginCommand command)
    {
        var result = await mediator.SendAsync<OwnerLoginCommand, ResponseLoggedOwnerJson>(command);
        return Ok(result);
    }
    
    [HttpPost]
    [Route("/auth/register/member")]
    [ProducesResponseType(typeof(ResponseLoggedUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> MemberRegister([FromServices] IMediator mediator,
        [FromBody] RegisterMemberCommand command)
    {
        var result = await mediator.SendAsync<RegisterMemberCommand, ResponseLoggedUserJson>(command);
        return Created(string.Empty, result);
    }  
    
    [HttpPost]
    [AuthenticatedUser(Roles.Admin)]
    [Route("/auth/register/users")]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UsersRegister([FromBody] RegisterUserCommand command, [FromServices] IMediator handler)
    {
        var result = await handler.SendAsync<RegisterUserCommand, ResponseRegisteredUserJson>(command);
        return Created(string.Empty, result);
    }  
}