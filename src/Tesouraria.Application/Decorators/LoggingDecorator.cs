using Microsoft.Extensions.Logging;
using Tesouraria.Domain.Abstractions.Mediator;

namespace Tesouraria.Application.Decorators;

public class LoggingDecorator<TReq, TRes>
    : IHandler<TReq, TRes> where TReq : IMessage
{
    private readonly IHandler<TReq, TRes> _inner;
    private readonly ILogger<LoggingDecorator<TReq, TRes>> _logger;

    public LoggingDecorator(
        IHandler<TReq, TRes> inner,
        ILogger<LoggingDecorator<TReq, TRes>> logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public async Task<TRes> HandleAsync(TReq command, CancellationToken ct = default)
    {
        _logger.LogInformation("➡️ Start {Command}", typeof(TReq).Name);

        var result = await _inner.HandleAsync(command, ct);

        _logger.LogInformation("✅ End {Command}", typeof(TReq).Name);

        return result;
    }
}