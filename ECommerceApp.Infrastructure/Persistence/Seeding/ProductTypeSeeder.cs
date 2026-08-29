using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Persistence.Contexts;
using ECommerceApp.Infrastructure.Persistence.Seeding.Models;

namespace ECommerceApp.Infrastructure.Persistence.Seeding;

public class ProductTypeSeeder(ECommerceDbContext dbContext) : IDataSeeder
{
    private readonly ECommerceDbContext _dbContext = dbContext;

    public int Order => 1;

    public async Task SeedAsync(CancellationToken ct = default)
        => await JsonSeeder.SeedIfEmptyAsync<ProductType, ProductTypeSeedModel>(
            _dbContext.Types,
            "types.json",
            m => (ProductType.Create(m.Name)).Value!,
            ct
            );
}