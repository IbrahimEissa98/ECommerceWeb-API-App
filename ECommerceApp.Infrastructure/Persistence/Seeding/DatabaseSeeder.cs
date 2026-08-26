using ECommerceApp.Infrastructure.Persistence.Contexts;

namespace ECommerceApp.Infrastructure.Persistence.Seeding;

public sealed class DatabaseSeeder(ECommerceDbContext dbContext,
                                   IEnumerable<IDataSeeder> dataSeeders)
{
    public async Task SeedAllAsync(CancellationToken ct = default)
    {
        foreach (var seeder in dataSeeders.OrderBy(ds => ds.Order))
        {
            await seeder.SeedAsync(ct);
            await dbContext.SaveChangesAsync(ct);
        }
    }
}
