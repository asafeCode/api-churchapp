using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Application.UseCases.Commands.User.Update;

public sealed class UpdateUserCommand : ICommand
{
    // Informações obrigatórias
    public required string Username { get; set; }
    public UserRole Role { get; set; }
    public DateOnly DateOfBirth { get; set; }

    // Informações pessoais
    public string? FullName { get; set; }
    public Gender? Gender { get; set; }
    public string? Phone { get; set; }
    public string? ProfessionalWork { get; set; }

    // Informações eclesiásticas
    public DateOnly? EntryDate { get; set; }
    public DateOnly? ConversionDate { get; set; }
    public bool? IsBaptized { get; set; }

    // Endereço
    public string? City { get; set; }
    public string? Neighborhood { get; set; }
}