#!/usr/bin/env bash
set -euo pipefail

ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$ROOT"

# Create solution
dotnet new sln -n CleanTodo

# Add projects
dotnet sln add src/CleanTodo.Domain/CleanTodo.Domain.csproj
dotnet sln add src/CleanTodo.Application/CleanTodo.Application.csproj
dotnet sln add src/CleanTodo.Infrastructure/CleanTodo.Infrastructure.csproj
dotnet sln add src/CleanTodo.WebApi/CleanTodo.WebApi.csproj

# Restore
dotnet restore

echo "Solution created: CleanTodo.sln"
