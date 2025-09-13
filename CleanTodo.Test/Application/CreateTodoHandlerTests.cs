using System;
using System.Threading;
using System.Threading.Tasks;
using CleanTodo.Application.Todos.Commands;
using CleanTodo.Test.Testing;
using FluentAssertions;
using Xunit;

namespace CleanTodo.Test.Application;

public class CreateTodoHandlerTests
{
    [Fact]
    public async Task Handle_Creates_Todo_And_Returns_Id()
    {
        using var ctx = AppDbContextFactory.Create();
        var handler = new CreateTodoHandler(ctx);

        var id = await handler.Handle(new CreateTodo("Acheter du lait"), CancellationToken.None);

        id.Should().NotBeEmpty();
        ctx.TodoItems.Should().ContainSingle(x => x.Id == id && x.Title == "Acheter du lait" && !x.IsDone);
    }
}