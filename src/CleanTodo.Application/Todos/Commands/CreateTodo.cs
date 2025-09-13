using CleanTodo.Domain.Entities;
using CleanTodo.Application.Abstractions;
using FluentValidation;
using MediatR;

namespace CleanTodo.Application.Todos.Commands;

public record CreateTodo(string Title) : IRequest<Guid>;

public sealed class CreateTodoValidator : AbstractValidator<CreateTodo>
{
    public CreateTodoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
    }
}

public sealed class CreateTodoHandler : IRequestHandler<CreateTodo, Guid>
{
    private readonly IAppDbContext _db;

    public CreateTodoHandler(IAppDbContext db) => _db = db;

    public async Task<Guid> Handle(CreateTodo request, CancellationToken ct)
    {
        var entity = new TodoItem(request.Title);
        _db.TodoItems.Add(entity);
        await _db.SaveChangesAsync(ct);
        return entity.Id;
    }
}
