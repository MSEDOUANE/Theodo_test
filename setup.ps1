param()
$ErrorActionPreference = "Stop"

$ROOT = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $ROOT

# Create solution
dotnet new sln -n CleanTodo

# Add projects
dotnet sln add src/CleanTodo.Domain/CleanTodo.Domain.csproj
dotnet sln add src/CleanTodo.Application/CleanTodo.Application.csproj
dotnet sln add src/CleanTodo.Infrastructure/CleanTodo.Infrastructure.csproj
dotnet sln add src/CleanTodo.WebApi/CleanTodo.WebApi.csproj

# Restore
dotnet restore

Write-Host "Solution created: CleanTodo.sln"
