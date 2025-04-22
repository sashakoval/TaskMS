# Use the official .NET 9 runtime as the base image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official .NET 9 SDK as the base image for building the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the project files
COPY ["src/TaskManagementSystem.Api/TaskManagementSystem.Api.csproj", "src/TaskManagementSystem.Api/"]
COPY ["src/TaskManagementSystem.Application/TaskManagementSystem.Application.csproj", "src/TaskManagementSystem.Application/"]
COPY ["src/TaskManagementSystem.Infrastructure/TaskManagementSystem.Infrastructure.csproj", "src/TaskManagementSystem.Infrastructure/"]
COPY ["src/TaskManagementSystem.Core/TaskManagementSystem.Core.csproj", "src/TaskManagementSystem.Core/"]

# Restore dependencies
RUN dotnet restore "src/TaskManagementSystem.Api/TaskManagementSystem.Api.csproj"

# Copy the rest of the source code
COPY src/. ./src/

# Build and publish
RUN dotnet publish "src/TaskManagementSystem.Api/TaskManagementSystem.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copy published files from build stage
COPY --from=build /app/publish .

# Expose the application on port 8080
EXPOSE 8080

# Set the entry point for the application
ENTRYPOINT ["dotnet", "TaskManagementSystem.Api.dll"]
