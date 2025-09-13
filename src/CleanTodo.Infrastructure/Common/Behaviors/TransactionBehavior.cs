using CleanTodo.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanTodo.Infrastructure.Common.Behaviors;

public sealed class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly AppDbContext _db;
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

    public TransactionBehavior(AppDbContext db, ILogger<TransactionBehavior<TRequest, TResponse>> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        var isQuery = typeof(TRequest).Name.EndsWith("Query", StringComparison.OrdinalIgnoreCase);
        if (isQuery) return await next();

        if (_db.Database.CurrentTransaction is not null)
            return await next();

        // For InMemory provider we don't have real transactions; still call next & SaveChanges for demo.
        try
        {
            var response = await next();
            await _db.SaveChangesAsync(ct);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Transaction failed for {Request}", typeof(TRequest).Name);
            throw;
        }
    }
}
