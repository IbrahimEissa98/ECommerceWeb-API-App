using ECommerceApp.Application.ProductTypes.DTOs;
using ECommerceApp.Domain.Common;
using MediatR;

namespace ECommerceApp.Application.ProductTypes.Queries;

public record GetByIdProductTypeQuery(int Id) : IRequest<Result<GetByIdProductTypeResponse>>;

public class GetByIdProductTypeQueryHandler(IProductTypeQueryService queryService)
    : IRequestHandler<GetByIdProductTypeQuery, Result<GetByIdProductTypeResponse>>
{
    public async Task<Result<GetByIdProductTypeResponse>> Handle(GetByIdProductTypeQuery request, CancellationToken cancellationToken)
    {
        var type = await queryService.GetByIdAsync(request.Id, cancellationToken);
        return type is null
            ? Error.NotFound("ProductType.NotFound", "Product Type was not found")
            : type;
    }
}



//public class GetByIdProductTypeQuery(IProductTypeQueryService typeQueryService)
//{
//    public async Task<Result<GetByIdProductTypeResponse>> ExecuteAsync(int id, CancellationToken ct = default)
//    {
//        var type = await typeQueryService.GetByIdAsync(id, ct);
//        return type is null
//            ? Error.NotFound("ProductType.NotFound", "Product Type was not found")
//            : type;
//    }
//}
