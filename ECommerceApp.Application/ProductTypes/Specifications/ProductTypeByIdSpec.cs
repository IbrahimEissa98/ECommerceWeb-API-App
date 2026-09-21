using ECommerceApp.Application.ProductTypes.DTOs;
using ECommerceApp.Application.ProductTypes.Mapping;
using ECommerceApp.Application.Specifications;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.ProductTypes.Specifications;

public class ProductTypeByIdSpec : Specification<ProductType, GetByIdProductTypeResponse>
{
    public ProductTypeByIdSpec(int id)
    {
        Query
            .AsNoTracking()
            .Where(t => t.Id == id)
            .Select(t => t.GetTypeByIdResponse());
    }
}
