using ECommerceApp.Application.Products.DTOs;
using ECommerceApp.Domain.Common;

namespace ECommerceApp.Application.Products.Queries;

public sealed class GetAllProductQuery(IProductQueryService productQueryService)
{
    private readonly IProductQueryService _productQueryService = productQueryService;

    public async Task<Result<IReadOnlyList<GetAllProductsResponse>>> ExecuteAsync(CancellationToken ct = default)
    {
        var products = await _productQueryService.GetAllAsync(ct);
        return Result<IReadOnlyList<GetAllProductsResponse>>.Success(products);
    }
}
