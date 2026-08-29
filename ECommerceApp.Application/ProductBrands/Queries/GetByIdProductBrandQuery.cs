using ECommerceApp.Application.ProductBrands.DTOs;
using ECommerceApp.Application.ProductTypes.DTOs;
using ECommerceApp.Domain.Common;

namespace ECommerceApp.Application.ProductBrands.Queries;

public class GetByIdProductBrandQuery(IProductBrandQueryService brandQueryService)
{
    public async Task<Result<GetByIdProductBrandResponse>> ExecuteAsync(int id, CancellationToken ct = default)
    {
        var brand = await brandQueryService.GetByIdAsync(id, ct);
        return brand is null
            ? Error.NotFound("Brand.NotFound", "Product brand was not found")
            : brand;
    }
}
