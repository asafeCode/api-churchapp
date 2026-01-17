using Microsoft.AspNetCore.Mvc;
using Tesouraria.API.Attributes;
using Tesouraria.API.Controllers.Base;
using Tesouraria.Application.UseCases.Commands.User.ChangePassword;
using Tesouraria.Application.UseCases.Commands.User.Update;
using Tesouraria.Application.UseCases.Queries.UserProfile;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses;
using Tesouraria.Domain.Dtos.Responses.Users;
using Tesouraria.Domain.Entities.Helpers;

namespace Tesouraria.API.Controllers.App;

[AuthenticatedUser(Roles.Admin, Roles.Member)]
public class UserController : TesourariaControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserProfile([FromServices] IMediator handler)
    {
        var query = new GetUserProfileQuery();
        var result = await handler.QueryAsync<GetUserProfileQuery, ResponseUserJson>(query);
        return Ok(result);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser([FromServices] IMediator handler,
        [FromBody] UpdateUserCommand command)
    {
        await handler.SendAsync(command);
        return NoContent();
    } 
    
    [HttpPatch("password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword([FromServices] IMediator handler,
        [FromBody] ChangePasswordRequest request)
    {
        var command = new ChangePasswordCommand(request);
        await handler.SendAsync(command);
        return NoContent();
    }  
}