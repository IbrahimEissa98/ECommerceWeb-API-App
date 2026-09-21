using ECommerceApp.Application.ProductBrands.DTOs;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.ProductBrands.Mapping;

public static class BrandMappingExtensions
{
    public static GetAllProductBrandsResponse GetBrandsResponse(this ProductBrand brand)
    {
        return new GetAllProductBrandsResponse
        {
            Id = brand.Id,
            Name = brand.Name
        };
    }

    public static GetByIdProductBrandResponse GetBrandResponse(this ProductBrand brand)
    {
        return new GetByIdProductBrandResponse
        {
            Id = brand.Id,
            Name = brand.Name
        };
    }
}
