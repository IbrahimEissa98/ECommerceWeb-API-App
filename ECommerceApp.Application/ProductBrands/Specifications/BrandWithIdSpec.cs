using ECommerceApp.Application.ProductBrands.DTOs;
using ECommerceApp.Application.ProductBrands.Mapping;
using ECommerceApp.Application.Specifications;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.ProductBrands.Specifications;

public class BrandWithIdSpec : Specification<ProductBrand, GetByIdProductBrandResponse>
{
    public BrandWithIdSpec(int id)
    {
        Query
            .AsNoTracking()
            .Where(b => b.Id == id)
            .Select(b => b.GetBrandResponse());
    }
}
