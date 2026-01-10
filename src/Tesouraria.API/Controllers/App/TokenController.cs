using Microsoft.AspNetCore.Mvc;
using Tesouraria.API.Controllers.Base;
using Tesouraria.Application.UseCases.Commands.Token.RefreshToken;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Users;

namespace Tesouraria.API.Controllers.App;

public class TokenController : TesourariaControllerBase
{
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(ResponseTokensJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshToken([FromServices] IMediator handler,
        [FromBody] CreateNewTokenCommand command)
    {
        var response = await handler.SendAsync<CreateNewTokenCommand, ResponseTokensJson>(command);
        return Ok(response);
    }
}