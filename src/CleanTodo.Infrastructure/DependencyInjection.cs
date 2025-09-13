using CleanTodo.Application.Abstractions;
using CleanTodo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTodo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString = null)
    {
        services.AddDbContext<AppDbContext>(o =>
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                o.UseInMemoryDatabase("CleanTodoDb");
            else
                o.UseInMemoryDatabase("CleanTodoDb"); // swap to .UseSqlServer(connectionString) etc.
        });

        services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());
        return services;
    }
}
