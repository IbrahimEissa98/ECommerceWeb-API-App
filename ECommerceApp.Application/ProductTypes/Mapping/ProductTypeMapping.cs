using ECommerceApp.Application.ProductTypes.DTOs;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.ProductTypes.Mapping;

public static class ProductTypeMapping
{
    public static GetAllProductTypesResponse GetTypesResponse(this ProductType productType)
    {
        return new GetAllProductTypesResponse { Id = productType.Id, Name = productType.Name };
    }

    public static GetByIdProductTypeResponse GetTypeByIdResponse(this ProductType productType)
    {
        return new GetByIdProductTypeResponse { Id = productType.Id, Name = productType.Name };
    }
}
