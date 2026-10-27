# Haven — Housing Operations Portal

**Independent full-stack portfolio demonstration** built with React 18, TypeScript, C#, ASP.NET Core 8 Minimal APIs, Entity Framework Core and SQL Server.

> This project is an original demonstration using fictional data. It is **not** a client deliverable, a production housing system, or a reproduction of proprietary source code.

## What it demonstrates

- Responsive enterprise dashboard with application status metrics and visual breakdown
- Application table with text search and status filters
- Create-application form with input validation
- Property directory derived from application records
- Typed React components, React hooks and responsive CSS
- REST endpoints for listing, retrieving, creating and summarizing applications
- EF Core persistence with SQL Server, indexed fields, async queries and cancellation support
- **Standalone frontend mode:** runs immediately with fictional data, even without .NET or SQL Server

## Repository structure

```text
frontend/                  React 18 + TypeScript + Vite UI
backend/HousingPortfolio.Api/   ASP.NET Core 8 API + EF Core
 database/                 Optional SQL Server setup script
```

## Quick start — frontend only

Requires Node.js 18+.

```bash
cd frontend
npm install
npm run dev
```

Open the local address printed by Vite (normally `http://localhost:5173`). Without a running backend, the UI loads fictional sample applications and stores changes in your browser's localStorage. This is a deliberate demo fallback.

## Run the backend with SQL Server

Requires .NET SDK 8, SQL Server (local or container), and the EF Core CLI (`dotnet tool install --global dotnet-ef --version 8.*`).

1. Update the `HousingDb` connection string in `backend/HousingPortfolio.Api/appsettings.json` for your local SQL Server instance. Never commit credentials.
2. From `backend/HousingPortfolio.Api`, run:

```bash
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run --launch-profile http
```

3. In another terminal, start the frontend as above. Vite proxies `/api` to `http://localhost:5080`.
4. API documentation: `http://localhost:5080/swagger`.

The API database starts empty. Add an application in the UI to populate it. If the API is unavailable, the frontend runs with its built-in fictional dataset.

## API endpoints

| Method | Route | Purpose |
|---|---|---|
| GET | `/api/applications` | List up to 200 applications, optional `status` and `search` filters |
| GET | `/api/applications/{id}` | Retrieve one application |
| POST | `/api/applications` | Validate and create an application |
| GET | `/api/summary` | Return status counts |

Example request:

```bash
curl -X POST http://localhost:5080/api/applications -H 'Content-Type: application/json' -d '{"applicant":"Demo Resident","property":"Example Residences","program":"Rental Assistance","units":1}'
```

## Scope and limitations

This is a **portfolio prototype**, not a production-ready application. Authentication, authorization, auditing, integration tests, pagination beyond the 200-record API limit, API error banners and deployment automation are outside this demo's scope. The backend uses SQL Server; the frontend fallback uses localStorage. Do not enter real personal information.

## Publishing on GitHub

1. Create a free GitHub account and a public repository named `housing-management-portfolio`.
2. Upload the contents of this folder to the repository (not the ZIP itself).
3. Add the repository URL to the top of your resume under **Portfolio / Code Samples**.
4. Optionally deploy the frontend to a static hosting service; a GitHub repository alone is enough to provide source samples, subject to the client's requirements.

## License

MIT — see `LICENSE`.
