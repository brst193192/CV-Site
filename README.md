# CV Site – Developer Portfolio API

An **ASP.NET Core (.NET 8) Web API** that lets developers showcase their portfolio by connecting to their GitHub account. It returns the developer's repositories and provides an advanced search across public GitHub repositories.

## Features

- **Portfolio** – all repositories of the configured GitHub user
- **Repository search** – search public GitHub repositories by name, language and user
- **Smart caching** – the portfolio is cached in memory and refreshed only when the user has new activity on GitHub, saving API calls
- Swagger UI for exploring the API

## Architecture

```
Api/              # Web API: controller, cached service decorator, Program.cs
CV Site/          # Service layer: GitHub integration with Octokit, options, DI extensions
IServise/         # Service interface
```

- **Decorator pattern** (with Scrutor) – `CachedGitHubService` wraps `GitHubService` and adds caching without changing it
- **Options pattern** for the GitHub user name and token
- **Dependency Injection** through interfaces

## Tech Stack

- ASP.NET Core Web API (.NET 8)
- Octokit (GitHub API client)
- IMemoryCache, Scrutor
- Swagger

## API

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/CvSite/GetPortfolio` | The configured user's repositories |
| GET | `/api/CvSite/SearchRepositories?userName=&repoName=&lang=` | Search public repositories |

## Getting Started

1. Create a GitHub personal access token.
2. Store your settings with user secrets (never commit the token):
   ```bash
   cd Api
   dotnet user-secrets set "GitHubIntegrationOptions:UserName" "<your-github-user>"
   dotnet user-secrets set "GitHubIntegrationOptions:Token" "<your-token>"
   ```
3. Run the API: `dotnet run --project Api` and open Swagger.
