using ECommerceApp.Application.ProductBrands.DTOs;
using ECommerceApp.Domain.Common;
using MediatR;

namespace ECommerceApp.Application.ProductBrands.Queries;

public record GetAllBrandsQuery : IRequest<Result<IReadOnlyList<GetAllProductBrandsResponse>>>;

public class GetAllBrandsQueryHandler(IProductBrandQueryService queryService)
    : IRequestHandler<GetAllBrandsQuery, Result<IReadOnlyList<GetAllProductBrandsResponse>>>
{
    public async Task<Result<IReadOnlyList<GetAllProductBrandsResponse>>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brands = await queryService.GetAllAsync(cancellationToken);
        return Result<IReadOnlyList<GetAllProductBrandsResponse>>.Success(brands);
    }
}


//public sealed class GetAllProductBrandsQuery(IProductBrandQueryService brandQueryService)
//{
//    private readonly IProductBrandQueryService _brandQueryService = brandQueryService;

//    public async Task<Result<IReadOnlyList<GetAllProductBrandsResponse>>> ExecuteAsync(CancellationToken ct = default)
//    {
//        var brands = await _brandQueryService.GetAllAsync(ct);
//        return Result<IReadOnlyList<GetAllProductBrandsResponse>>.Success(brands);
//    }
//}
