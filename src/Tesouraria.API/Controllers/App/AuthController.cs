using Microsoft.AspNetCore.Mvc;
using Tesouraria.API.Attributes;
using Tesouraria.API.Controllers.Base;
using Tesouraria.Application.UseCases.Commands.Auth.OwnerLogin;
using Tesouraria.Application.UseCases.Commands.Auth.UserLogin;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses;
using Tesouraria.Domain.Dtos.Responses.Users;
using Tesouraria.Domain.Entities.Helpers;

namespace Tesouraria.API.Controllers.App;

public class AuthController : TesourariaControllerBase
{
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(typeof(ResponseLoggedUserJson),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromServices] IMediator handler, [FromBody] DoLoginCommand command)
    {
        var result = await handler.SendAsync<DoLoginCommand, ResponseLoggedUserJson>(command);
        return Ok(result);
    }
    
    [HttpPost]
    [Route("owner/login")]
    [ProducesResponseType(typeof(ResponseLoggedOwnerJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromServices] IMediator mediator,
        [FromBody] OwnerLoginCommand command)
    {
        var result = await mediator.SendAsync<OwnerLoginCommand, ResponseLoggedOwnerJson>(command);
        return Ok(result);
    }
}