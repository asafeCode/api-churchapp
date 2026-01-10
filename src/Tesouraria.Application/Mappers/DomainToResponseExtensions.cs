using Tesouraria.Domain.Dtos.ReadModels;
using Tesouraria.Domain.Dtos.Responses.Inflows;
using Tesouraria.Domain.Dtos.Responses.Outflows;
using Tesouraria.Domain.Dtos.Responses.Tenants;
using Tesouraria.Domain.Dtos.Responses.Users;
using Tesouraria.Domain.Entities;
using Tesouraria.Domain.Entities.Globals;
using Tesouraria.Domain.Extensions;

namespace Tesouraria.Application.Mappers;

public static class DomainToRequestExtensions
{
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
    public static ResponseOutflowsJson ToResponse(this IEnumerable<OutflowDashboardReadModel> outflows)
    {
        var response = outflows.Select(outflow => new ResponseShortOutflowJson(
            outflow.Id,
            outflow.ExpenseName,
            outflow.Date,
            outflow.Amount,
            outflow.PaymentMethod,
            outflow.CurrentInstallment,
            outflow.TotalInstallments)); 
        
        return new ResponseOutflowsJson { Outflows = response };
    }       
    public static ResponseTenantsJson ToResponse(this IEnumerable<Tenant> tenants)
    {
        var response = tenants.Select(tenant => new ResponseTenantJson(
            tenant.Id,
            tenant.Name
            ));

        return new ResponseTenantsJson(response);
    }       
    public static ResponseInflowsJson ToResponse(this IEnumerable<InflowDashboardReadModel> inflowReadModel)
    {
        var response = inflowReadModel.Select(inflow => new ResponseShortInflowJson(
            inflow.Id,
            inflow.WorshipId ?? Guid.Empty,
            inflow.MemberId ?? Guid.Empty,
            inflow.Date,
            inflow.InflowType,
            inflow.MemberName,
            WorshipInfo: inflow.WorshipDay.HasValue 
                ? $"{inflow.WorshipDay} | {inflow.WorshipTime}" : "—",
            inflow.PaymentMethod,
            inflow.Amount));
        
        return new ResponseInflowsJson { Inflows = response };
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