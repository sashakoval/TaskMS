# Task Management System

A .NET 9 RESTful API built following Clean Architecture principles.

## Project Structure
- **TaskManagementSystem.Api** - REST API endpoints and handler
- **TaskManagementSystem.Application** - Application business logic and use cases
- **TaskManagementSystem.Core** - Domain entities and business rules
- **TaskManagementSystem.Infrastructure** - External concerns and implementations

## Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/products/docker-desktop/)
- [Docker Compose](https://docs.docker.com/compose/install/)
- [Azure Service Bus](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-overview)
## Getting Started

### Running with Docker Compose
1. Clone the repository:
```bash
git clone https://github.com/yourusername/TaskMS.git
cd TaskMS
```
2. Start the application and database:
```bash
docker compose up
```
3. The API will be available at `http://localhost:8080`.

### Running Locally

1. Install dependencies:
```bash
dotnet restore
```
2. Update the connection string in `appsettings.json` to point to your local SQL Server instance

3. Run the application:
```bash
cd src/TaskManagementSystem.Api
dotnet run
```

## API Documentation
Once the application is running, you can access the Swagger documentation at:
- Docker: `http://localhost:8080/swagger`
- Local: `http://localhost:5000/swagger`

## Environment Variables
The application uses the following environment variables:
- `ASPNETCORE_ENVIRONMENT`: Application environment (Development/Production)
- `ConnectionStrings__SQLServer`: SQL Server connection string
- `ServiceBus__ConnectionString`: Azure service bus connection string
- `ServiceBus__QueueName`: Azure service bus queue name
   
## Docker Configuration
- The API runs on port 8080
- SQL Server database runs on port 1433
- Default database credentials:
  - Database: sqlserver
  - Username: sa
  - Password: 12345

## Project Features
- Clean Architecture implementation
- REST API endpoints for managing tasks
- SQL Server database integration
- Swagger documentation
- Docker containerization

## Technologies Used
- .NET 9
- Entity Framework Core
- SQL Server
- Docker
- Swagger/OpenAPI
- Azure Service Bus

## Author
Oleksandr Koval
