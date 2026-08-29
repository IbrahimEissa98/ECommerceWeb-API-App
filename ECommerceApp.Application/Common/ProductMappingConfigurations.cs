using ECommerceApp.Application.ProductBrands.DTOs;
using ECommerceApp.Application.Products.DTOs;
using ECommerceApp.Application.ProductTypes.DTOs;
using ECommerceApp.Domain.Entities;
using Mapster;

namespace ECommerceApp.Application.Common;

public class ProductMappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, GetAllProductsResponse>()
            .Map(d => d.Brand, s => s.ProductBrand.Name)
            .Map(d => d.Type, s => s.ProductType.Name);

        config.NewConfig<Product, GetByIdProductResponse>()
            .Map(d => d.Brand, s => s.ProductBrand.Name)
            .Map(d => d.Type, s => s.ProductType.Name);


        config.NewConfig<ProductBrand, GetAllProductBrandsResponse>();
        config.NewConfig<ProductBrand, GetByIdProductBrandResponse>();


        config.NewConfig<ProductBrand, GetAllProductTypesResponse>();
        config.NewConfig<ProductBrand, GetByIdProductTypeResponse>();
    }
}
