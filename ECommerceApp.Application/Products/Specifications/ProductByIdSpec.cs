using ECommerceApp.Application.Products.DTOs;
using ECommerceApp.Application.Specifications;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.Products.Specifications;

public class ProductByIdSpec : Specification<Product, GetByIdProductResponse>
{
    public ProductByIdSpec(Guid id)
    {
        Query
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new GetByIdProductResponse(p.Id, p.Name, p.Description, p.Images.Select(i => i.Url).ToArray(), p.Price, p.ProductBrand.Name, p.ProductType.Name));
    }
}
