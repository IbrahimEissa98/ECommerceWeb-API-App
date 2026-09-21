using ECommerceApp.Application.Products.DTOs;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.Products.Mapping;

public static class ProductMapping
{
    public static GetAllProductsResponse GetProductsResponse(this Product product)
    {
        return new GetAllProductsResponse(product.Id, product.Name, product.Description, [.. product.Images.Select(i => i.Url)], product.Price, product.ProductBrand.Name, product.ProductType.Name);
    }

    public static GetByIdProductResponse GetProductByIdResponse(this Product product)
    {
        return new GetByIdProductResponse(product.Id, product.Name, product.Description, [.. product.Images.Select(i => i.Url)], product.Price, product.ProductBrand.Name, product.ProductType.Name);
    }
}
