using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Persistence.Contexts;
using ECommerceApp.Infrastructure.Persistence.Seeding.Models;

namespace ECommerceApp.Infrastructure.Persistence.Seeding;

public class ProductBrandSeeder(ECommerceDbContext dbContext) : IDataSeeder
{
    private readonly ECommerceDbContext _dbContext = dbContext;

    public int Order => 2;

    public async Task SeedAsync(CancellationToken ct = default)
        => await JsonSeeder.SeedIfEmptyAsync<ProductBrand, ProductBrandSeedModel>(
            _dbContext.Brands,
            "brands.json",
            m => (ProductBrand.Create(m.Name)).Value!,
            ct: ct
            );
}
