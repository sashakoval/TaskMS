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
        var context = services.GetRequiredService<TaskDbContext>();
        var logger = services.GetRequiredService<ILogger<TaskDbContext>>();
        var retryCount = 10;
        var delaySeconds = 5;

        for (int i = 0; i < retryCount; i++)
        {
            try
            {
                if (context.Database.IsSqlServer())
                {
                    logger.LogInformation("Checking for pending migrations...");
                    await context.Database.MigrateAsync();
                    logger.LogInformation("Database migrations applied successfully.");
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Database is not ready yet. Waiting {Delay}s before retry ({Attempt}/{MaxAttempts})", delaySeconds, i + 1, retryCount);
                await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
            }
        }

        throw new Exception("Unable to connect to the database.");
    }
}
