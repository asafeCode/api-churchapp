using Tesouraria.Domain.Entities.Enums;

namespace Tesouraria.Domain.Dtos.Responses.Users;

public sealed record ResponseUserJson(
    Guid Id,
    string Username,
    string? FullName,
    UserRole Role,
    DateOnly DateOfBirth,
    int Age,

    // Informações pessoais
    Gender? Gender,
    string? Phone,
    string? ProfessionalWork,

    // Informações eclesiásticas
    DateOnly? EntryDate,
    DateOnly? ConversionDate,
    int? ConversionTime,
    bool? IsBaptized,

    // Endereço
    string? City,
    string? Neighborhood
);