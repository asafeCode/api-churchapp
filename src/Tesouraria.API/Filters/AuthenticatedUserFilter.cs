using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Tesouraria.API.Attributes;
using Tesouraria.Domain.Dtos.Responses;
using Tesouraria.Domain.Exceptions;
using Tesouraria.Domain.Exceptions.ExceptionsBase;
using Tesouraria.Domain.Extensions;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Token;

namespace Tesouraria.API.Filters;

public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
{
    private readonly IAccessTokenValidator _tokenValidator;
    private readonly IUserReadRepository _userRepository;

    public AuthenticatedUserFilter(IAccessTokenValidator tokenValidator, IUserReadRepository repository)
    {
        _tokenValidator = tokenValidator;
        _userRepository = repository;
    }
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context);
            var claims = _tokenValidator.ValidateUserToken(token);
            var endpointRoles = context.ActionDescriptor.EndpointMetadata
                .OfType<AuthenticatedUserAttribute>()
                .SelectMany(a => a.Roles)
                .ToArray();
            
            if (endpointRoles.Length > 0 && endpointRoles.Contains(claims.UserRole).IsFalse())
                throw new UnauthorizedException(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
            
            var userRole = await _userRepository.GetActiveUserRoleById(claims.UserId, claims.TenantId);
            
            if (userRole is null || userRole.ToString() != claims.UserRole)
                throw new UnauthorizedException(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
        }
        catch (SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson("TokenIsExpired")
            {
                TokenIsExpired = true,
            });
        }
        catch (TesourariaException tarefasCrudException)
        {
            context.HttpContext.Response.StatusCode = (int)tarefasCrudException.GetStatusCode();
            context.Result = new ObjectResult(new ResponseErrorJson(tarefasCrudException.GetErrorMessages()));
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE));
        }
    }
    private static string TokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();
        
        return string.IsNullOrWhiteSpace(authentication) ? 
            throw new UnauthorizedException(ResourceMessagesException.NO_TOKEN) 
            : 
            authentication["Bearer ".Length..].Trim();
    }
}