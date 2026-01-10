using Tesouraria.Domain.Abstractions.Mediator;
using Tesouraria.Domain.Repositories;

namespace Tesouraria.Application.Decorators;

public sealed class TransactionDecorator<TReq, TRes>
    : IHandler<TReq, TRes>
    where TReq : ICommand
{
    private readonly IHandler<TReq, TRes> _inner;
    private readonly IUnitOfWork _uow;

    public TransactionDecorator(
        IHandler<TReq, TRes> inner,
        IUnitOfWork uow)
    {
        _inner = inner;
        _uow = uow;
    }

    public async Task<TRes> HandleAsync(TReq request, CancellationToken ct)
    {
        await _uow.BeginAsync(ct);

        try
        {
            var result = await _inner.HandleAsync(request, ct);
            await _uow.CommitAsync(ct);
            return result;
        }
        catch
        {
            await _uow.RollbackAsync(ct);
            throw;
        }
    }
}