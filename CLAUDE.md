# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

EventsHub is a course project (ICI 2026 01): an ASP.NET Core (.NET 10) REST API over SQLite via EF Core, plus a React 19 + TypeScript + Vite + MUI frontend in `web/`. Commits follow the pattern `feat(PARCIAL-NN) - message` on per-assignment branches (`PARCIAL-02`, etc.). Helper scripts and some docs are written in Spanish.

## Commands

Backend (run from repo root unless noted):

```powershell
dotnet build EventsHub.slnx
dotnet test tests/EventsHub.UnitTests
dotnet test tests/EventsHub.UnitTests --filter "FullyQualifiedName~EventsControllerTests.GetEventDetailAsync_WhenEventExists_ReturnsMatchingEvent"
.\restart-api.ps1            # kill stray dotnet/MSBuild processes, then `dotnet watch` in src/EventsHub.Api
.\restart-api.ps1 -Reset     # also delete eventshub.db so seed data is regenerated (new GUIDs)
.\restart-api.ps1 -Reset -Clean
.\stop-api.ps1               # kill all dotnet/EventsHub/VBCSCompiler/MSBuild processes (fixes locked-file build errors)
```

The API listens on `https://localhost:5001`; routes are under `/api/v1/[controller]`.

EF Core migrations (Persistence holds the DbContext, Api is the startup project):

```powershell
dotnet ef migrations add <Name> --project src/EventsHub.Persistence --startup-project src/EventsHub.Api
```

Migrations are applied and seed data inserted automatically on API startup (`Program.cs` calls `MigrateAsync` + `DbInitializer.SeedData`, which only seeds when the `Events` table is empty).

Frontend (from `web/`):

```powershell
npm run dev      # Vite on port 3000 (HTTPS via vite-plugin-mkcert)
npm run build    # tsc -b && vite build
npm run lint
```

`web/.env` (gitignored) must define `VITE_API_URL=https://localhost:5001/api/v1`. The API's CORS policy only allows `http(s)://localhost:3000`.

## Architecture

Projects (`EventsHub.slnx`):

- `EventsHub.Domain` — entities (`Event`, string GUID `Id`). No dependencies.
- `EventsHub.Persistence` — `AppDbContext`, `DbInitializer` seed data, EF migrations. References Domain.
- `EventsHub.Application` (folder is `src/EventsHubApplication/`, no dot) — currently empty placeholder.
- `EventsHub.Api` — controllers inject `AppDbContext` directly (no service/mediator layer yet). All controllers inherit `EventsHubBaseController`, which supplies `[ApiController]` and the `api/v1/[controller]` route. SQLite file `eventshub.db` lives in `src/EventsHub.Api/` and is excluded from the build.
- `EventsHub.OpenApi` — standalone NSwag host that loads the Api assembly as an application part purely to generate the OpenAPI document; it is not the running API.

JSON is camelCase on the wire while C# properties are PascalCase; the frontend types in `web/src/lib/Types/index.d.ts` mirror the camelCase shape.

### OpenAPI / generated client

Full procedure is in [docs/OpenApi 1.md](docs/OpenApi%201.md). Short version:

1. `dotnet run --project src/EventsHub.OpenApi/EventsHub.OpenApi.csproj --no-launch-profile --urls http://127.0.0.1:5011`
2. `Invoke-WebRequest -Uri http://127.0.0.1:5011/swagger/EventsHub/swagger.json -OutFile src/openapi/EventsHub.v1.json`
3. Stop the host (`Get-Process -Name "EventsHub.OpenApi" | Stop-Process -Force`)
4. `dotnet tool restore; dotnet tool run nswag run src/nswag/EventsHub.nswag`

`src/openapi/EventsHub.v1.json` and `*.generated.cs` are generated — never hand-edit. Note the nswag `output` path is resolved relative to `src/nswag/`, so the client actually lands in `src/src/EventsHub.OpenApi/Generated/`. When referencing an Api type for the assembly, use a public controller type (`Program` is internal).

## Tests

- `tests/EventsHub.UnitTests` (NUnit 4) — not pure unit tests: `GlobalTestSetup` (`[SetUpFixture]`) creates a real SQLite `eventshub.db` in the test output directory, migrates and seeds it, and exposes a shared static `AppDbContext`. Controllers are instantiated directly with that context; tests must not assume fixed IDs.
- `tests/EventHub.integrationTests` — a Bruno (OpenCollection YAML) collection run against the live API using the `local` environment (`baseUrl=https://localhost:5001/api/v1`). Not part of the .NET solution. The "Get - 200" request hard-codes an event GUID, so it must be updated after reseeding the database (`restart-api.ps1 -Reset`).
