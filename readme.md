# TrueStory

This is a sample **ASP.NET Core Web API** project that exposes a simple REST API with three endpoints:

- `GET /objects` — retrieve a list of items
- `POST /objects` — add a new item
- `DELETE /objects/{id}` — delete an item by ID

By default, the project starts with **Swagger UI** enabled, which provides a self-documenting interface for exploring and testing the API.

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download) or later
- Optional: [Visual Studio 2022+](https://visualstudio.microsoft.com/) with *ASP.NET and web development* workload

---

## Getting Started

### Run from Command Line

```bash
dotnet build
dotnet run
```

The application will start on:

https://localhost:5001
or http://localhost:5000
Run from Visual Studio
Open the .sln file in Visual Studio.
Set the API project as the Startup Project (if not already).
Press F5 or click Start Debugging.
Swagger

When the application starts, it automatically launches Swagger UI in your browser at:

https://localhost:5001/swagger
Swagger is a built-in, self-documenting tool that allows you to:

See all available API endpoints
Test requests directly from the browser
View request/response schemas
No additional configuration is required.

**Notes**:
- All requests must be sent as JSON.
- The `/items` endpoint stores data in memory.
- Swagger UI is available at `/swagger`.
