using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Tesouraria.Application.Decorators;
using Tesouraria.Application.UseCases.Commands.Expense.Create;
using Tesouraria.Application.UseCases.Commands.Inflow.Create;
using Tesouraria.Application.UseCases.Commands.Outflow.Create;
using Tesouraria.Application.UseCases.Commands.User.ChangePassword;
using Tesouraria.Application.UseCases.Commands.User.Update;
using Tesouraria.Application.UseCases.Commands.Users.Create;
using Tesouraria.Application.UseCases.Commands.Worship.Create;
using Tesouraria.Domain.Abstractions.Mediator;

namespace Tesouraria.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        RegisterHandlers(services);
        RegisterValidators(services);
        RegisterDecorators(services);
    }
    private static void RegisterHandlers(IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<AssemblyMarker>()
            .AddClasses(classes => classes.AssignableTo(typeof(IHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
        
    }
    private static void RegisterDecorators(IServiceCollection services)
    {
        services.Decorate(typeof(IHandler<,>), typeof(LoggingDecorator<,>));
        services.Decorate(typeof(IHandler<,>), typeof(ValidationDecorator<,>));
        services.Decorate(typeof(IHandler<,>), typeof(TransactionDecorator<,>));
    }
    private static void RegisterValidators(IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateWorshipValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateOutflowValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateInflowValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateExpenseValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateUserValidator>();
        services.AddValidatorsFromAssemblyContaining<ChangePasswordValidator>(); 
    }
}