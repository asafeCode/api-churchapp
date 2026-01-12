using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Tesouraria.Domain.Abstractions.Mediator;

namespace Tesouraria.Application.Decorators;

public class TimingDecorator<TReq, TRes> : IHandler<TReq, TRes>
    where TReq : IMessage
{
    private readonly IHandler<TReq, TRes> _inner;
    private readonly ILogger<TimingDecorator<TReq, TRes>> _logger;

    public TimingDecorator(
        IHandler<TReq, TRes> inner,
        ILogger<TimingDecorator<TReq, TRes>> logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public async Task<TRes> HandleAsync(TReq request, CancellationToken ct = default)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            return await _inner.HandleAsync(request, ct);
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation(
                "Executed {Request} -> {Response} in {ElapsedMs} ms",
                typeof(TReq).Name,
                typeof(TRes).Name,
                stopwatch.ElapsedMilliseconds);
        }
    }
}