using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaskManagementSystem.Infrastructure.Data;

public static class TaskDbInitializer
{
    public static async Task InitializeDatabaseAsync(IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<TaskDbContext>();
            var logger = services.GetRequiredService<ILogger<TaskDbContext>>();

            if (context.Database.IsSqlServer())
            {
                logger.LogInformation("Checking for pending migrations...");
                await context.Database.MigrateAsync();
                logger.LogInformation("Database migrations applied successfully.");
            }
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<TaskDbContext>>();
            logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }
}
