using Microsoft.EntityFrameworkCore;
using Tinkr.Api.Repositories;
namespace Tinkr.Api.Data;

public static class DataExtensions
{
    public static async Task InitializeDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        
        var dbContext = scope.ServiceProvider.GetRequiredService<TinkrProjectsContext>();
        await dbContext.Database.MigrateAsync();

        var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DB Initializer");
        logger.LogInformation(5, "Database initialized");

    }
    public static IServiceCollection AddRepository(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("TinkrProjectsContext");
        services.AddSqlServer<TinkrProjectsContext>(connString).AddScoped<IProjectsRepository, EntityFrameworkProjectsRepository>();
        return services;
    }
}
