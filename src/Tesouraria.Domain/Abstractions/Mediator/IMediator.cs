namespace Tesouraria.Domain.Abstractions.Mediator;

public interface IMediator
{
    public Task<Unit> SendAsync<TCommand>(TCommand command, CancellationToken token = default)
        where TCommand : ICommand;

    public Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken token = default)
        where TCommand : ICommand;

    public Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken token = default)
        where TQuery : IQuery;
}