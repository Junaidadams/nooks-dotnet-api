# Minimal API Template

A reusable .NET minimal API template scaffolded from the production-proven
`hssg-dotnet-integration-api` structure, stripped of domain-specific business
logic so it can be used as the starting point for any future minimal API.

## Install the template (one time)

From this folder:

```bash
dotnet new install .
```

## Create a new API

```bash
dotnet new minapi -n MyNewApi -o ./src/MyNewApi
cd ./src/MyNewApi
dotnet run
```

This replaces `NooksApi` with `MyNewApi` across project, solution,
namespace, and assembly names.

### Template options

There are no template parameters — `dotnet new` handles renaming automatically via
`-n`/`--name` (which replaces `NooksApi` with your project name across
project, solution, namespace, and assembly names).

All infrastructure (EF Core + Npgsql, Serilog, Swagger/OpenAPI, API key auth,
rate limiting, FluentValidation, global exception handlers) is included by
default. Remove any folders/files you don't need (e.g. `Authentication/`,
`BackgroundJobs/`, `Data/`) and their references in `Program.cs`.

## Structure

The template follows the same folder-by-concern layout as the source repo:

- `Authentication/` — API key handler + auth extensions
- `BackgroundJobs/` — hosted service skeletons
- `Constants/` — endpoint names/tags, rate-limit policies, etc.
- `Data/` — DbContext, entities, configurations, enums
- `Dtos/` — request/response DTOs
- `Endpoints/` — `IEndpoint` interface + endpoint classes grouped by domain
- `Exceptions/` — domain exceptions + global exception handlers
- `Extensions/` — service registration extensions (logging, OpenAPI, etc.)
- `Handlers/` / `Helpers/` — shared helpers
- `Migrations/` — EF Core migrations
- `Options/` — strongly-typed options classes
- `Queues/` — channel-based queues
- `Services/` — business logic services
- `Validators/` — FluentValidation validators

## Docker

Build and run:

```bash
docker build -t mynewapi .
docker run -p 8080:8080 mynewapi
```

Local Postgres for development:

```bash
docker-compose -f deploy/docker-compose.local.yml up -d
```
