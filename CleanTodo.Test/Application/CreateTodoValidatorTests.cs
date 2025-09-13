using CleanTodo.Application.Todos.Commands;
using FluentAssertions;
using Xunit;

namespace CleanTodo.Test.Application;

public class CreateTodoValidatorTests
{
    private readonly CreateTodoValidator _validator = new();

    [Fact]
    public void Empty_Title_Is_Invalid()
    {
        var result = _validator.Validate(new CreateTodo(""));
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Too_Long_Title_Is_Invalid()
    {
        var result = _validator.Validate(new CreateTodo(new string('x', 201)));
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Valid_Title_Is_Valid()
    {
        var result = _validator.Validate(new CreateTodo("OK"));
        result.IsValid.Should().BeTrue();
    }
}