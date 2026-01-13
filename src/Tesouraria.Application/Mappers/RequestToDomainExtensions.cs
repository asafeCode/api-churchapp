using Tesouraria.Application.UseCases.Commands.Auth.Register.Member;
using Tesouraria.Application.UseCases.Commands.Auth.Register.Users;
using Tesouraria.Application.UseCases.Commands.Expense.Create;
using Tesouraria.Application.UseCases.Commands.Inflow.Create;
using Tesouraria.Application.UseCases.Commands.Outflow.Create;
using Tesouraria.Application.UseCases.Commands.User.Update;
using Tesouraria.Application.UseCases.Commands.Worship.Create;
using Tesouraria.Domain.Entities;
using Tesouraria.Domain.Entities.Enums;
using Tesouraria.Domain.Entities.ValueObjects;
using Tesouraria.Domain.Services.Security;

namespace Tesouraria.Application.Mappers;

public static class RequestToDomainExtensions
{
    public static User ToUser(this RegisterUserCommand request, IPasswordEncripter passwordEncripter, Guid tenantId)
    {
        return User.Create(
            username: request.Name,
            passwordHash: passwordEncripter.Encrypt(request.Password),
            role: request.Role,
            dateOfBirth: DateOnly.MinValue,
            tenantId: tenantId);
    }  
    
    public static User ToMember(this RegisterMemberCommand request, IPasswordEncripter passwordEncripter, Guid tenantId)
    {
        return User.Create(
            username: request.Name,
            passwordHash: passwordEncripter.Encrypt(request.Password),
            role: UserRole.Member,
            dateOfBirth: DateOnly.MinValue, 
            tenantId: tenantId
            );
    }    
    
    public static void ToUpdatedUser(this UpdateUserCommand command, User user)
    {
        user.Update(
            command.Username,
            command.DateOfBirth,
            command.FullName,
            command.Gender,
            command.Phone,
            command.ProfessionalWork,
            command.EntryDate,
            command.ConversionDate,
            command.IsBaptized,
            command.City,
            command.Neighborhood
        );
    }
    public static Inflow ToInflow(this CreateInflowCommand request, Guid userId, Guid tenantId)
    {
        return new Inflow
        {
            Date = request.Date,
            Type = request.Type,
            PaymentMethod = request.PaymentMethod,
            Amount = request.Amount,
            Description = request.Description,
            WorshipId = request.WorshipId,
            MemberId = request.UserId,
            CreatedByUserId = userId,
            TenantId = tenantId
        };
    }    
    
    public static Outflow ToOutflow(this CreateOutflowCommand request, Guid userId, Guid tenantId, Expense expense)
    {
        bool isInstallment = expense.Type == ExpenseType.Installment;
        decimal? amount = isInstallment ? expense.AmountOfEachInstallment! : request.Amount;
        int? currentInstallmentPayed = isInstallment ? expense.CurrentInstallment : null;
        return new Outflow
        {
            Date = request.Date,
            PaymentMethod = request.PaymentMethod,
            Amount = amount!.Value,
            Description = request.Description,
            CurrentInstallmentPayed = currentInstallmentPayed,
            ExpenseId = request.ExpenseId,
            CreatedByUserId = userId,
            TenantId = tenantId
        };
    }   
    public static Expense ToExpense(this CreateExpenseCommand request, Guid tenantId)
    {
        return new Expense
        {
            Name = request.Name,
            Type = request.Type,
            CurrentInstallment = request.CurrentInstallment,
            AmountOfEachInstallment = request.AmountOfEachInstallment,
            TotalInstallments = request.TotalInstallments,
            TenantId = tenantId,
        };
    }    
    public static Worship ToWorship(this CreateWorshipCommand request, Guid tenantId)
    {
        return new Worship
        {
            DayOfWeek = request.DayOfWeek,
            Time = request.Time,
            Description = request.Description,
            TenantId = tenantId,
        };
    }
}