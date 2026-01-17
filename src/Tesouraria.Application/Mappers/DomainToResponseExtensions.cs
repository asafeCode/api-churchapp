using Tesouraria.Domain.Dtos.ReadModels;
using Tesouraria.Domain.Dtos.Responses.Inflows;
using Tesouraria.Domain.Dtos.Responses.Outflows;
using Tesouraria.Domain.Dtos.Responses.Tenants;
using Tesouraria.Domain.Dtos.Responses.Users;
using Tesouraria.Domain.Entities;
using Tesouraria.Domain.Entities.Globals;
using Tesouraria.Domain.Entities.ValueObjects;
using Tesouraria.Domain.Extensions;
using Tesouraria.Domain.Services.Token;

namespace Tesouraria.Application.Mappers;

public static class DomainToRequestExtensions
{
    public static ResponseLoggedUserJson ToLoggedResponse(this User user, RefreshToken refresh,
        IAccessTokenGenerator tokenGenerator)
    {
        return new ResponseLoggedUserJson
        {
            Name = user.Username,
            Role = user.Role,
            Tokens = new ResponseTokensJson
            {
                AccessToken = tokenGenerator.Generate(new JwtClaims(user.Id, user.TenantId, user.Role.ToString())),
                RefreshToken = refresh.Value
            }
        };
    } 
    public static ResponseUserJson ToResponse(this User user)
    {
        return new ResponseUserJson(
            Id: user.Id,
            Username: user.Username,
            FullName: user.FullName,
            Role: user.Role,
            DateOfBirth: user.DateOfBirth,
            Age: user.Age,

            Gender: user.Gender,
            Phone: user.Phone,
            ProfessionalWork: user.ProfessionalWork,

            EntryDate: user.EntryDate,
            ConversionDate: user.ConversionDate,
            ConversionTime: user.ConversionTime,
            IsBaptized: user.IsBaptized,

            City: user.City,
            Neighborhood: user.Neighborhood
        );
    } 
    public static IEnumerable<ResponseShortOutflowJson> ToResponse(this IEnumerable<OutflowDashboardReadModel> outflows)
    {
        var response = outflows.Select(outflow => new ResponseShortOutflowJson(
            outflow.Id,
            outflow.Description,
            outflow.ExpenseType,
            outflow.ExpenseName,
            outflow.Date,
            outflow.Amount,
            outflow.PaymentMethod,
            outflow.CurrentInstallment,
            outflow.TotalInstallments)); 
        
        return response;
    }       
    public static ResponseTenantsJson ToResponse(this IEnumerable<Tenant> tenants)
    {
        var response = tenants.Select(tenant => new ResponseTenantJson(
            tenant.Id,
            tenant.Name
            ));

        return new ResponseTenantsJson(response);
    }       
    public static IEnumerable<ResponseShortInflowJson> ToResponse(this IEnumerable<InflowDashboardReadModel> inflowReadModel)
    {
        var response = inflowReadModel.Select(inflow => new ResponseShortInflowJson(
            inflow.Id,
            inflow.Description,
            inflow.Date,
            inflow.InflowType,
            inflow.MemberName,
            WorshipInfo: inflow.WorshipDay.HasValue 
                ? $"{inflow.WorshipDay.ToPortuguese()} | {inflow.WorshipTime}" : "—",
            inflow.PaymentMethod,
            inflow.Amount));
        
        return response;
    }       
    
    public static ResponseUsersJson ToResponse(this IEnumerable<User>? users)
    {
        if (users is null) return new ResponseUsersJson();
        var response = users.Select(user => new ResponseUserProfileForDashboardJson
        {
            Id = user.Id,
            Name = user.Username,
            DateOfBirth = user.DateOfBirth,
            Role = user.Role,
        });
        
        return new ResponseUsersJson { Users = response };
    }    
}