using ECommerceApp.Application.ProductBrands.DTOs;
using ECommerceApp.Application.ProductBrands.Specifications;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Repositories;
using MediatR;

namespace ECommerceApp.Application.ProductBrands.Queries;

public record GetByIdBrandQueryV2(int Id) : IRequest<Result<GetByIdProductBrandResponse>>;

public class GetByIdBrandQueryHandlerV2(IReadRepository<ProductBrand, int> repo)
    : IRequestHandler<GetByIdBrandQueryV2, Result<GetByIdProductBrandResponse>>
{
    public async Task<Result<GetByIdProductBrandResponse>> Handle(GetByIdBrandQueryV2 request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
            return Error.Validation("Brand.Id", "Invalid brand id");

        var brand = await repo.FirstOrDefaultAsync<GetByIdProductBrandResponse>(new BrandWithIdSpec(request.Id), cancellationToken);
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
