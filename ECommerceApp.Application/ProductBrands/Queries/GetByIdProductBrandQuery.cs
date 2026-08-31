using ECommerceApp.Application.ProductBrands.DTOs;
using ECommerceApp.Domain.Common;
using MediatR;

namespace ECommerceApp.Application.ProductBrands.Queries;

public record GetByIdBrandQuery(int Id) : IRequest<Result<GetByIdProductBrandResponse>>;

public class GetByIdBrandQueryHandler(IProductBrandQueryService queryService)
    : IRequestHandler<GetByIdBrandQuery, Result<GetByIdProductBrandResponse>>
{
    public async Task<Result<GetByIdProductBrandResponse>> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
    {
        var brand = await queryService.GetByIdAsync(request.Id, cancellationToken);
        return brand is null
            ? Error.NotFound("Brand.NotFound", "Product brand was not found")
            : brand;
    }
}



//public class GetByIdProductBrandQuery(IProductBrandQueryService brandQueryService)
//{
//    public async Task<Result<GetByIdProductBrandResponse>> ExecuteAsync(int id, CancellationToken ct = default)
//    {
//        var brand = await brandQueryService.GetByIdAsync(id, ct);
//        return brand is null
//            ? Error.NotFound("Brand.NotFound", "Product brand was not found")
//            : brand;
//    }
//}
