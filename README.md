# CleanTodo (.NET 8 • Clean Architecture • MediatR • EF Core)

A tiny, production-ready starter with **Clean Architecture** layers:

- **Domain** — business entities & rules (pure C#)
- **Application** — CQRS via MediatR, validators, DTOs
- **Infrastructure** — EF Core (InMemory), persistence & DI
- **WebApi** — Minimal API hosting, Swagger, composition root
- **Pipeline Behaviors** — Validation, Logging, Performance, Transaction

## Quickstart

### Prereqs
- .NET SDK 8.x

### 1) Create solution & wire projects (scripted)
Use the script for your OS from the repo root:

**macOS/Linux**
```bash
chmod +x setup.sh
./setup.sh
```

**Windows (PowerShell)**
```powershell
./setup.ps1
```

> These scripts create `CleanTodo.sln`, add the projects, and run `dotnet restore`.

### 2) Run the API
```bash
dotnet run --project src/CleanTodo.WebApi
```

- Swagger UI: https://localhost:5001/swagger
- Create Todo:
  ```bash
  curl -X POST https://localhost:5001/api/todos -H "Content-Type: application/json" -d "{"title":"Buy milk"}"
  ```
- List Todos:
  ```bash
  curl https://localhost:5001/api/todos
  ```
- Mark Done:
  ```bash
  curl -X POST https://localhost:5001/api/todos/{id}/done
  ```

## Switch to a real database
In `Infrastructure/DependencyInjection.cs`, replace the InMemory provider with your DB provider (e.g., SQL Server / Postgres), then add EF Core packages and migrations.

## Project Layout
```
src/
  CleanTodo.Domain/
  CleanTodo.Application/
  CleanTodo.Infrastructure/
  CleanTodo.WebApi/
```

## License
MIT
