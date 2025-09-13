using CleanTodo.Application.Abstractions;
using CleanTodo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanTodo.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Title).IsRequired().HasMaxLength(200);
            b.Property(x => x.IsDone);
            b.Property(x => x.CreatedAtUtc);
        });
    }
}
