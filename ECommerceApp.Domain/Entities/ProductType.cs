using ECommerceApp.Domain.Common;

namespace ECommerceApp.Domain.Entities;

public class ProductType : BaseEntity<int>
{
    public string Name { get; private set; } = default!;

    public ICollection<Product> Products { get; private set; } = [];
}
