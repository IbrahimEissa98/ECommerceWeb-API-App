namespace ECommerceApp.Infrastructure.Persistence.Seeding.Models;

public class ProductSeedModel
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string PictureUrl { get; set; } = default!;
    public decimal Price { get; set; }
    public int BrandId { get; set; }
    public int TypeId { get; set; }
}
