using ECommerceApp.Application.Common.Services;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Persistence.Contexts;
using ECommerceApp.Infrastructure.Persistence.Seeding.Models;

namespace ECommerceApp.Infrastructure.Persistence.Seeding;

public class ProductSeeder(ECommerceDbContext dbContext, IImageService imageService) : IDataSeeder
{
    private readonly ECommerceDbContext _dbContext = dbContext;
    private readonly IImageService _imageService = imageService;

    public int Order => 3;

    public async Task SeedAsync(CancellationToken ct = default)
    {
        await JsonSeeder.SeedIfEmptyAsync<Product, ProductSeedModel>(
                _dbContext.Products,
                "products.json",
                m => (Product.Create(m.Name, m.Description, m.Price, m.BrandId, m.TypeId)).Value!,
                hasImages: true,
                dbContext: _dbContext,
                imageService: _imageService,
                ct: ct
                );
    }
}