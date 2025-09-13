using CleanTodo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanTodo.Application.Abstractions;

public interface IAppDbContext
{
    DbSet<TodoItem> TodoItems { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
