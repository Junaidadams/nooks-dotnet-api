# Nooks API

Backend for [nooks.co.za](https://nooks.co.za), a halaal restaurant and takeaway directory for Cape Town.

ASP.NET Core 10 Minimal API · EF Core + Npgsql + PostGIS · ASP.NET Core Identity (cookie auth) · Serilog · FluentValidation

## Prerequisites

- .NET 10 SDK
- Docker
- EF Core CLI: `dotnet tool install --global dotnet-ef`

## Run locally

Start Postgres (with PostGIS) and Mailpit:

```bash
docker compose -f deploy/docker-compose.local.yml up -d
```

Run the API (migrations are applied on startup):

```bash
dotnet run --project NooksApi
```

| Service | URL |
|---|---|
| API | http://localhost:5102 |
| OpenAPI document | http://localhost:5102/openapi/v1.json |
| Liveness | http://localhost:5102/health/live |
| Readiness | http://localhost:5102/health/ready |
| Mailpit inbox | http://localhost:8025 |

## Configuration

| Setting | Where |
|---|---|
| `ConnectionStrings:Default` | `appsettings.Development.json` locally; `ConnectionStrings__Default` env var in production |
| Local secrets (Resend, R2, Turnstile) | `dotnet user-secrets --project NooksApi set "<Key>" "<value>"` |
| `NOOKS_SKIP_MIGRATIONS=true` | Skips applying migrations on startup |

## Migrations

```bash
dotnet ef migrations add <Name> --project NooksApi --output-dir Data/Migrations
dotnet ef database update --project NooksApi
```

## Tests

```bash
dotnet test
```

## Deployment

- API: Docker image on Render (`Dockerfile`), served at `api.nooks.co.za`
- Database: Neon Postgres (Frankfurt) with PostGIS
- CI: GitHub Actions (`.github/workflows`)
