using Microsoft.Extensions.DependencyInjection;
using Tesouraria.Domain.Abstractions.Mediator;

namespace Tesouraria.Infrastructure.Services.Mediator;

public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task<Unit> SendAsync<TCommand>(TCommand command, CancellationToken token = default)
        where TCommand : ICommand
    {
        var handler = _serviceProvider.GetRequiredService<IHandler<TCommand, Unit>>();
        return await handler.HandleAsync(command, token);
    }
    public async Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken token = default)
        where TCommand : ICommand
    {
        var handler = _serviceProvider.GetRequiredService<IHandler<TCommand, TResult>>();
        return await handler.HandleAsync(command, token);
    }
    public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken token = default)
        where TQuery : IQuery
    {
        var handler = _serviceProvider.GetRequiredService<IHandler<TQuery, TResult>>();
        return await handler.HandleAsync(query, token);
    }
}