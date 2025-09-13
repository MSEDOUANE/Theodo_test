using CleanTodo.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTodo.Application.Todos.Commands;

public record MarkDone(Guid Id) : IRequest<Unit>; // Fix: Specify the generic type parameter for IRequest

public sealed class MarkDoneHandler : IRequestHandler<MarkDone, Unit>
{
    private readonly IAppDbContext _db;
    public MarkDoneHandler(IAppDbContext db) => _db = db;

    public async Task<Unit> Handle(MarkDone request, CancellationToken ct)
    {
        var entity = await _db.TodoItems.FirstOrDefaultAsync(x => x.Id == request.Id, ct);
        if (entity is null) throw new KeyNotFoundException("Todo not found.");

        entity.MarkDone();
        await _db.SaveChangesAsync(ct);
        return Unit.Value;
    }
}
