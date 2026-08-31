using ECommerceApp.Application.ProductTypes.DTOs;
using ECommerceApp.Domain.Common;
using MediatR;

namespace ECommerceApp.Application.ProductTypes.Queries;

public record GetAllProductTypesQuery : IRequest<Result<IReadOnlyList<GetAllProductTypesResponse>>>;

public class GetAllProductTypesQueryHandler(IProductTypeQueryService queryService)
    : IRequestHandler<GetAllProductTypesQuery, Result<IReadOnlyList<GetAllProductTypesResponse>>>
{
    public async Task<Result<IReadOnlyList<GetAllProductTypesResponse>>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
    {
        var types = await queryService.GetAllAsync(cancellationToken);
        return Result<IReadOnlyList<GetAllProductTypesResponse>>.Success(types);
    }
}



//public sealed class GetAllProductTypesQuery(IProductTypeQueryService typeQueryService)
//{
//    private readonly IProductTypeQueryService _typeQueryService = typeQueryService;

//    public async Task<Result<IReadOnlyList<GetAllProductTypesResponse>>> ExecuteAsync(CancellationToken ct = default)
//    {
//        var types = await _typeQueryService.GetAllAsync(ct);
//        return Result<IReadOnlyList<GetAllProductTypesResponse>>.Success(types);
//    }
//}
