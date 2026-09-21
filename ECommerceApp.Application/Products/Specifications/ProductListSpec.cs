using ECommerceApp.Application.Products.DTOs;
using ECommerceApp.Application.Specifications;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.Products.Specifications;

public class ProductListSpec : Specification<Product, GetAllProductsResponse>
{
    public ProductListSpec()
    {
        Query
            .AsNoTracking()
            .OrderBy(p => p.Name)

            .Select(p => new GetAllProductsResponse(p.Id, p.Name, p.Description, p.Images.Select(i => i.Url).ToArray(), p.Price, p.ProductBrand.Name, p.ProductType.Name));
    }
}
