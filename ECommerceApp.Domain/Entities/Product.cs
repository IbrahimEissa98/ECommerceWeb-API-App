using ECommerceApp.Domain.Common;

namespace ECommerceApp.Domain.Entities;

public class Product : BaseEntity<Guid>
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string PictureUrl { get; private set; } = default!;
    public decimal Price { get; private set; }

    public int BrandId { get; private set; }
    public ProductBrand ProductBrand { get; private set; } = default!;

    public int TypeId { get; private set; }
    public ProductType ProductType { get; private set; } = default!;
}
