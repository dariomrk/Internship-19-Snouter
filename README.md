# Internship-19-Snouter

## Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)

- [Docker](https://www.docker.com/)

- IDE (preferably [Visual Studio](https://visualstudio.microsoft.com/))

## Technologies used:

- ASP.NET Core
- EF Core
- Postgres

## Overview

The solution is separated into four projects:

- **Api:**
  
  Consists of controllers that handle incoming requests from clients and return appropriate responses. The controllers are responsible for processing the user input, calling the appropriate application logic, and returning the output to the client.

- **Application:**
  
  Contains the core business logic and is responsible for processing data and orchestrating workflows. The application layer consists of services that encapsulate a set of related operations.

- **Data:**
  
  Responsible for persisting and retrieving data from storage. It includes a database context that encapsulates the data access logic, and repositories that abstract away the details of data access.

- **Common:**

  Contains common functionality and dependencies.
  
## Get started developing

### Database

In the solution root directory run:

 `docker build -t snouter-development-database -f Dockerfile.Development .` 

Afterwards run:

`docker run -d -p 5432:5432 --name snouter-development-database snouter-development-database`

These commands will create and run a Docker container containing a Postgres database.

### Environments

In the Package Manager Console execute:

`$env:ASPNETCORE_ENVIRONMENT = "Development"`

### Migrations

After you have set up your models and the `SnouterDbContext.cs`, open the Package Manager Console, change the Default project to `Data` and execute: `Add-Migration <migration-name>` followed by: `Update-Database`.
