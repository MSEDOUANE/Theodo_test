using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CleanTodo.Application.Common.Behaviors;

public sealed class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;
    private readonly int _thresholdMs;

    public PerformanceBehavior(ILogger<PerformanceBehavior<TRequest, TResponse>> logger, int thresholdMs = 300)
    {
        _logger = logger;
        _thresholdMs = thresholdMs;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        var sw = Stopwatch.StartNew();
        var response = await next();
        sw.Stop();

        if (sw.ElapsedMilliseconds > _thresholdMs)
        {
            _logger.LogWarning("Long running request {Request} took {Elapsed} ms", typeof(TRequest).Name, sw.ElapsedMilliseconds);
        }

        return response;
    }
}
