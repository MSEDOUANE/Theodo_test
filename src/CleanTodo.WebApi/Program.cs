using System.Reflection;
using CleanTodo.Application.Common.Behaviors;
using CleanTodo.Application.Todos.Commands;
using CleanTodo.Application.Todos.Queries;
using CleanTodo.Infrastructure;
using CleanTodo.Infrastructure.Common.Behaviors;
using FluentValidation;
using MediatR;


var builder = WebApplication.CreateBuilder(args);

// MediatR: register handlers from Application
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateTodo).Assembly);
});

// Pipeline behaviors
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

// FluentValidation

builder.Services.AddValidatorsFromAssembly(typeof(CreateTodo).Assembly);

// Infrastructure
builder.Services.AddInfrastructure(); // InMemory by default

// Web
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Minimal endpoints over Application layer
app.MapPost("/api/todos", async (CreateTodo cmd, ISender sender, CancellationToken ct) =>
{
    var id = await sender.Send(cmd, ct);
    return Results.Created($"/api/todos/{id}", new { id });
});

app.MapGet("/api/todos", async (bool? isDone, ISender sender, CancellationToken ct) =>
{
    var result = await sender.Send(new GetTodos(isDone), ct);
    return Results.Ok(result);
});

app.MapPost("/api/todos/{id:guid}/done", async (Guid id, ISender sender, CancellationToken ct) =>
{
    await sender.Send(new MarkDone(id), ct);
    return Results.NoContent();
});

app.Run();
