namespace ECommerceApp.Infrastructure.Persistence.Seeding.Models;

public class ProductSeedModel
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<string> Pictures { get; set; } = [];
    public decimal Price { get; set; }
    public int BrandId { get; set; }
    public int TypeId { get; set; }
}
