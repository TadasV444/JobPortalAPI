# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

**Run the API:**
```powershell
dotnet run --launch-profile http   # http://localhost:5237
dotnet run --launch-profile https  # https://localhost:7115
```

**Database (PostgreSQL via Docker):**
```powershell
docker-compose up -d               # start PostgreSQL 16 on port 5432
```

**EF Core migrations:**
```powershell
dotnet ef migrations add <name>
dotnet ef database update
```

Configuration goes in `appsettings.json` (see `appsettings.example.json` for required keys). Swagger UI is available at `/swagger` when running.

There are no automated tests in this project.

## Architecture

ASP.NET Core 9 Web API using a three-layer Clean Architecture:

- **Api/** — Controllers, request/response DTOs, `ApiResponse<T>` wrapper with extension methods
- **Core/** — Domain entities, the `Role` enum, and service interfaces (contracts only)
- **Infrastructure/** — EF Core `JobPortalContext`, service implementations, migrations

All services are scoped and registered in `Program.cs`. Controllers depend on service interfaces from `Core/Interfaces/`, which are implemented in `Infrastructure/Services/`.

### Database

PostgreSQL via EF Core (Npgsql). The `JobPortalContext` defines 8 tables:
- `Users` — login credentials, role, refresh token fields
- `CandidateProfiles` / `EmployerProfiles` — 1-to-1 with `Users`
- `Skills` — skill catalog (unique name)
- `CandidateSkills` / `JobSkills` — many-to-many join tables (composite unique constraints)
- `JobPostings` — many-to-1 with `EmployerProfiles`
- `Applications` — join between `CandidateProfiles` and `JobPostings` (unique per pair)

All relationships use cascade delete.

### Authentication

JWT (HS512) issued by `AuthService` with 4-hour access token expiry. A refresh token (24-hour expiry) is stored on the `Users` row and returned alongside the access token. `POST /api/auth/refresh-token` validates the stored token and issues a new pair. Passwords are hashed with ASP.NET Core `PasswordHasher<T>`.

The JWT payload carries `Email`, `UserId` (as `NameIdentifier`), and `Role` claims. Protect endpoints with `[Authorize]`; role-based control is done with `[Authorize(Roles = "...")]`.

### API Response Shape

Every controller response uses the `ApiResponse<T>` wrapper (defined in `Api/Extentions/`):

```json
{
  "success": true,
  "statusCode": 200,
  "message": "...",
  "data": { ... },
  "errors": [],
  "timestamp": "..."
}
```

Use the static extension methods (`.Ok()`, `.NotFound()`, `.BadRequest()`, etc.) rather than constructing `ApiResponse<T>` directly.

### Known Issues

- The namespace segment "Infractructure" is misspelled in several files (actual folder is `Infrastructure`). Match the existing spelling when editing those files.
- Several DTOs contain `// will add later` comments marking planned fields.
