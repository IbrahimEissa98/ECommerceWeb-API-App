using ECommerceApp.Application.ProductTypes.DTOs;
using ECommerceApp.Domain.Common;

namespace ECommerceApp.Application.ProductTypes.Queries;

public class GetByIdProductTypeQuery(IProductTypeQueryService typeQueryService)
{
    public async Task<Result<GetByIdProductTypeResponse>> ExecuteAsync(int id, CancellationToken ct = default)
    {
        var type = await typeQueryService.GetByIdAsync(id, ct);
        return type is null
            ? Error.NotFound("ProductType.NotFound", "Product Type was not found")
            : type;
    }
}
