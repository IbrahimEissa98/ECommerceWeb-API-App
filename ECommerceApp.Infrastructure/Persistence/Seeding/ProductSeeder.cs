using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Persistence.Contexts;
using ECommerceApp.Infrastructure.Persistence.Seeding.Models;

namespace ECommerceApp.Infrastructure.Persistence.Seeding;

public class ProductSeeder(ECommerceDbContext dbContext) : IDataSeeder
{
    private readonly ECommerceDbContext _dbContext = dbContext;

    public int Order => 3;

    public async Task SeedAsync(CancellationToken ct = default)
        => await JsonSeeder.SeedIfEmptyAsync<Product, ProductSeedModel>(
            _dbContext.Products,
            "products.json",
            m => (Product.Create(m.Name, m.Description, m.PictureUrl, m.Price, m.BrandId, m.TypeId)).Value!,
            ct
            );
}