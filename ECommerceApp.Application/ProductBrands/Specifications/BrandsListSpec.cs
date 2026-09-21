using ECommerceApp.Application.ProductBrands.DTOs;
using ECommerceApp.Application.ProductBrands.Mapping;
using ECommerceApp.Application.Specifications;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.ProductBrands.Specifications;

public sealed class BrandsListSpec : Specification<ProductBrand, GetAllProductBrandsResponse>
{
    public BrandsListSpec()
    {
        Query.AsNoTracking().Select(b => b.GetBrandsResponse());
    }
}
