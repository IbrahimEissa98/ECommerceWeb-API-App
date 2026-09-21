using ECommerceApp.Application.Messaging.Abstractions;
using ECommerceApp.Application.ProductTypes.DTOs;
using ECommerceApp.Application.ProductTypes.Specifications;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Repositories;

namespace ECommerceApp.Application.ProductTypes.Queries;

public record GetByIdProductTypeQueryV2(int Id) : IQuery<Result<GetByIdProductTypeResponse>>;

public class GetByIdProductTypeQueryHandlerV2(IReadRepository<ProductType, int> repo)
    : IRequestHandler<GetByIdProductTypeQueryV2, Result<GetByIdProductTypeResponse>>
{
    public async Task<Result<GetByIdProductTypeResponse>> Handle(GetByIdProductTypeQueryV2 request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
            return Error.Validation("Type.Id", "Invalid type id");

        var type = await repo.FirstOrDefaultAsync<GetByIdProductTypeResponse>
            (new ProductTypeByIdSpec(request.Id), cancellationToken);
        return type is null
            ? Error.NotFound("ProductType.NotFound", "Product Type was not found")
            : type;
    }
}
