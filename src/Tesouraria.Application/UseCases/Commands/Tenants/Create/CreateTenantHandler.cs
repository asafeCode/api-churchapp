using FluentValidation;
using Tesouraria.Application.Mappers;
using Tesouraria.Application.UseCases.Commands.Users.Create;
using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Dtos.Responses.Tenants;
using Tesouraria.Domain.Entities.Enums;
using Tesouraria.Domain.Entities.Globals;
using Tesouraria.Domain.Extensions;
using Tesouraria.Domain.Repositories;
using Tesouraria.Domain.Repositories.Owner;
using Tesouraria.Domain.Repositories.User;
using Tesouraria.Domain.Services.Logged;
using Tesouraria.Domain.Services.Security;

namespace Tesouraria.Application.UseCases.Commands.Tenants.Create;

public class CreateTenantHandler : ICommandHandler<CreateTenantCommand, ResponseCreatedTenantJson>
{
    private readonly ILoggedOwner _loggedOwner;
    private readonly IOwnerRepository _ownerRepository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUserWriteRepository _userRepository;

    public CreateTenantHandler(ILoggedOwner loggedOwner, IOwnerRepository ownerRepository, IUserWriteRepository userRepository, IUnitOfWork unitOfWork, IPasswordEncripter passwordEncripter)
    {
        _loggedOwner = loggedOwner;
        _ownerRepository = ownerRepository;
        _userRepository = userRepository;
        _passwordEncripter = passwordEncripter;
    }
    public async Task<ResponseCreatedTenantJson> HandleAsync(CreateTenantCommand command, CancellationToken ct = default)
    {
        if (command.Name.IsEmpty())
            throw new ValidationException("Nome não pode ser vazio");
        
        var ownerId = _loggedOwner.Owner();
        var tenant = new Tenant
        {
            Name = command.Name,
            OwnerId = ownerId
        };
        var userCommand = new RegisterUserCommand
        {
            Name = "admin",
            Password = "admin123",
            DateOfBirth = DateOnly.MinValue,
            Role = UserRole.Admin
        };
        var user = userCommand.ToUser(_passwordEncripter, tenant.Id);
        
        await _ownerRepository.AddTenant(tenant, ct);
        await _userRepository.AddUserAsync(user);
        
        return new ResponseCreatedTenantJson(tenant.Id, tenant.Name);
    }
}