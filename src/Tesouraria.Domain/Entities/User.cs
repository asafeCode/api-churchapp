using System.ComponentModel.DataAnnotations.Schema;
using Tesouraria.Domain.Entities.Enums;
using Tesouraria.Domain.Entities.Helpers;
using Tesouraria.Domain.Extensions;

namespace Tesouraria.Domain.Entities;

public class User : EntityBase
{
    private User(){}
    // Informações Obrigatórias
    public string Username { get; private set; } = string.Empty;
    public string NormalizedUsername { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public UserRole Role { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    
    [NotMapped]
    public int Age => UserHelper.CalculateAge(DateOfBirth, DateTime.Today.ToDateOnly());

    // Informações pessoais
    public string? FullName { get; private set;}
    public Gender? Gender { get; private set; }
    public string? Phone { get; private set; }
    public string? ProfessionalWork { get; private set; }

    // Informações eclesiásticas
    public DateOnly? EntryDate { get; private set; }
    public DateOnly? ConversionDate { get; private set; }
    
    [NotMapped]
    public int? ConversionTime => ConversionDate.HasValue ? UserHelper.CalculateAge(DateOfBirth, ConversionDate.Value) : null;
    public bool? IsBaptized { get; private set; }

    // Endereço
    public string? City { get; private set;}
    public string? Neighborhood { get; private set;}
    
    public void UpdatePassword(string newPasswordHash) => PasswordHash = newPasswordHash;
    public void Update(
        string username,
        UserRole role,
        DateOnly dateOfBirth,
        string? fullName,
        Gender? gender,
        string? phone,
        string? professionalWork,
        DateOnly? entryDate,
        DateOnly? conversionDate,
        bool? isBaptized,
        string? city,
        string? neighborhood)
    {
        Username = username;
        NormalizedUsername = username.NormalizeUsername();
        Role = role;
        DateOfBirth = dateOfBirth;

        FullName = fullName;
        Gender = gender;
        Phone = phone;
        ProfessionalWork = professionalWork;

        EntryDate = entryDate;
        ConversionDate = conversionDate;
        IsBaptized = isBaptized;

        City = city;
        Neighborhood = neighborhood;
    }   
    public static User Create(
        string username,
        string passwordHash,
        UserRole role,
        DateOnly dateOfBirth,
        Guid tenantId)
    {
        return new User
        {
            Username = username,
            NormalizedUsername = username.NormalizeUsername(),
            PasswordHash = passwordHash,
            Role = role,
            DateOfBirth = dateOfBirth,
            TenantId = tenantId
        };
    }
}