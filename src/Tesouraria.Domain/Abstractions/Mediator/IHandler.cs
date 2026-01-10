namespace Tesouraria.Domain.Abstractions.Mediator;

public interface IHandler<in TRequest, TResponse> where TRequest : IMessage
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken ct = default);
}

public interface ICommandHandler<in TCommand>
    : IHandler<TCommand, Unit>
    where TCommand : ICommand { }

public interface ICommandHandler<in TCommand, TResult>
    : IHandler<TCommand, TResult>
    where TCommand : ICommand { }

public interface IQueryHandler<in TQuery, TResult>
    : IHandler<TQuery, TResult>
    where TQuery : IQuery { }

public readonly struct Unit
{
    public static readonly Unit Value = new();
}