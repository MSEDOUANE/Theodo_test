using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanTodo.Application.Todos.Commands;
using CleanTodo.Application.Todos.Queries;
using CleanTodo.Infrastructure.Persistence;
using CleanTodo.Test.Testing;
using FluentAssertions;
using Xunit;

namespace CleanTodo.Test.Application;

public class GetTodosHandlerTests
{
    private async Task SeedAsync(AppDbContext ctx)
    {
        var create = new CreateTodoHandler(ctx);
        var id1 = await create.Handle(new CreateTodo("A faire 1"), CancellationToken.None);
        var id2 = await create.Handle(new CreateTodo("A faire 2"), CancellationToken.None);
        var mark = new MarkDoneHandler(ctx);
        await mark.Handle(new MarkDone(id2), CancellationToken.None);
    }

    [Fact]
    public async Task Returns_All_When_Filter_Null()
    {
        using var ctx = AppDbContextFactory.Create();
        await SeedAsync(ctx);

        var handler = new GetTodosHandler(ctx);
        var list = await handler.Handle(new GetTodos(null), CancellationToken.None);

        list.Should().HaveCount(2);
    }

    [Fact]
    public async Task Filters_Done()
    {
        using var ctx = AppDbContextFactory.Create();
        await SeedAsync(ctx);

        var handler = new GetTodosHandler(ctx);
        var done = await handler.Handle(new GetTodos(true), CancellationToken.None);

        done.Should().HaveCount(1);
        done.Single().IsDone.Should().BeTrue();
    }

    [Fact]
    public async Task Filters_Pending()
    {
        using var ctx = AppDbContextFactory.Create();
        await SeedAsync(ctx);

        var handler = new GetTodosHandler(ctx);
        var pending = await handler.Handle(new GetTodos(false), CancellationToken.None);

        pending.Should().HaveCount(1);
        pending.Single().IsDone.Should().BeFalse();
    }
}