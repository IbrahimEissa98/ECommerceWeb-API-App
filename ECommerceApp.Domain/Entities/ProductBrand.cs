using ECommerceApp.Domain.Common;

namespace ECommerceApp.Domain.Entities;

public class ProductBrand : BaseEntity<int>
{
    public string Name { get; private set; } = default!;

    public ICollection<Product> Products { get; private set; } = [];

    private ProductBrand() { }

    public static ProductBrand Create(string name)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(name);

        return new() { Name = name.Trim() };
    }
}
