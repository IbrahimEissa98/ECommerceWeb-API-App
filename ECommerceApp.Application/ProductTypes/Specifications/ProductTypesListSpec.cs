using ECommerceApp.Application.ProductTypes.DTOs;
using ECommerceApp.Application.ProductTypes.Mapping;
using ECommerceApp.Application.Specifications;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.ProductTypes.Specifications;

public class ProductTypesListSpec : Specification<ProductType, GetAllProductTypesResponse>
{
    public ProductTypesListSpec()
    {
        Query
            .AsTracking()
            .Select(t => t.GetTypesResponse());
    }
}
