using ECommerceApp.Application.Products.DTOs;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Errors;

namespace ECommerceApp.Application.Products.Queries;

public class GetByIdProductQuery(IProductQueryService productQueryService)
{
    public async Task<Result<GetByIdProductResponse>> ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var product = await productQueryService.GetByIdAsync(id, ct);
        return product is null ? ProductErrors.NotFound : product;
    }
}
