using System;
using CleanTodo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanTodo.Test.Testing;

internal static class AppDbContextFactory
{
    public static AppDbContext Create()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

        return new AppDbContext(options);
    }
}