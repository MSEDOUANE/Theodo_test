using System;
using System.Threading;
using System.Threading.Tasks;
using CleanTodo.Application.Todos.Commands;
using CleanTodo.Domain.Entities;
using CleanTodo.Test.Testing;
using FluentAssertions;
using Xunit;

namespace CleanTodo.Test.Application;

public class MarkDoneHandlerTests
{
    [Fact]
    public async Task Handle_Sets_IsDone_To_True()
    {
        using var ctx = AppDbContextFactory.Create();
        var todo = new TodoItem("Test");
        ctx.TodoItems.Add(todo);
        await ctx.SaveChangesAsync();

        var handler = new MarkDoneHandler(ctx);
        await handler.Handle(new MarkDone(todo.Id), CancellationToken.None);

        (await ctx.TodoItems.FindAsync(todo.Id))!.IsDone.Should().BeTrue();
    }

    [Fact]
    public async Task Unknown_Id_Throws_KeyNotFound()
    {
        using var ctx = AppDbContextFactory.Create();
        var handler = new MarkDoneHandler(ctx);

        var act = async () => await handler.Handle(new MarkDone(Guid.NewGuid()), CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage("Todo not found.");
    }
}