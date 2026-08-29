using ECommerceApp.Application.ProductBrands.DTOs;
using ECommerceApp.Application.ProductTypes.DTOs;
using ECommerceApp.Domain.Common;

namespace ECommerceApp.Application.ProductBrands.Queries;

public sealed class GetAllProductBrandsQuery(IProductBrandQueryService brandQueryService)
{
    private readonly IProductBrandQueryService _brandQueryService = brandQueryService;

    public async Task<Result<IReadOnlyList<GetAllProductBrandsResponse>>> ExecuteAsync(CancellationToken ct = default)
    {
        var brands = await _brandQueryService.GetAllAsync(ct);
        return Result<IReadOnlyList<GetAllProductBrandsResponse>>.Success(brands);
    }
}
