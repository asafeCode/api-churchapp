using FluentValidation;
using Tesouraria.Domain.Abstractions.Mediator;

namespace Tesouraria.Application.Decorators;

public sealed class ValidationDecorator<TCommand, TResult> : IHandler<TCommand, TResult>
    where TCommand : ICommand
{
    private readonly IHandler<TCommand, TResult> _inner;
    private readonly IEnumerable<IValidator<TCommand>> _validators;

    public ValidationDecorator(
        IHandler<TCommand, TResult> inner,
        IEnumerable<IValidator<TCommand>> validators)
    {
        _inner = inner;
        _validators = validators;
    }

    public async Task<TResult> HandleAsync(TCommand command, CancellationToken ct = default)
    {
        foreach (var validator in _validators)
        {
            await validator.ValidateAndThrowAsync(command, ct);
        }

        return await _inner.HandleAsync(command, ct);
    }
}