using ECommerceApp.Application.ProductTypes.DTOs;
using ECommerceApp.Domain.Common;

namespace ECommerceApp.Application.ProductTypes.Queries;

public sealed class GetAllProductTypesQuery(IProductTypeQueryService typeQueryService)
{
    private readonly IProductTypeQueryService _typeQueryService = typeQueryService;

    public async Task<Result<IReadOnlyList<GetAllProductTypesResponse>>> ExecuteAsync(CancellationToken ct = default)
    {
        var types = await _typeQueryService.GetAllAsync(ct);
        return Result<IReadOnlyList<GetAllProductTypesResponse>>.Success(types);
    }
}
