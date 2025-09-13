using CleanTodo.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTodo.Application.Todos.Queries;

public record GetTodos(bool? IsDone = null) : IRequest<IReadOnlyList<TodoDto>>;

public sealed class TodoDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = "";
    public bool IsDone { get; init; }
    public DateTime CreatedAtUtc { get; init; }
}

public sealed class GetTodosHandler : IRequestHandler<GetTodos, IReadOnlyList<TodoDto>>
{
    private readonly IAppDbContext _db;

    public GetTodosHandler(IAppDbContext db) => _db = db;

    public async Task<IReadOnlyList<TodoDto>> Handle(GetTodos request, CancellationToken ct)
    {
        var query = _db.TodoItems.AsNoTracking();
        if (request.IsDone is not null)
            query = query.Where(t => t.IsDone == request.IsDone);

        return await query
            .OrderByDescending(t => t.CreatedAtUtc)
            .Select(t => new TodoDto
            {
                Id = t.Id,
                Title = t.Title,
                IsDone = t.IsDone,
                CreatedAtUtc = t.CreatedAtUtc
            })
            .ToListAsync(ct);
    }
}
