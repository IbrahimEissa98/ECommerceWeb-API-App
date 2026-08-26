using ECommerceApp.Infrastructure.Persistence.Contexts;
using ECommerceApp.Infrastructure.Persistence.Seeding;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.API.Extensions;

public static class AppSeederExtensions
{
    public static async Task SeedDatabaseAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ECommerceDbContext>();
        var dbSeeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();

        if (app.Environment.IsDevelopment())
        {
            await dbContext.Database.MigrateAsync();
            await dbSeeder.SeedAllAsync();
        }
        else
            await dbSeeder.SeedAllAsync();
    }
}
