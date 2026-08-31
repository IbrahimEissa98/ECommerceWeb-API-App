using ECommerceApp.Application.Products.DTOs;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Errors;
using MediatR;

namespace ECommerceApp.Application.Products.Queries;

public record GetByIdProductQuery(Guid Id) : IRequest<Result<GetByIdProductResponse>>;

public class GetByIdProductHandler(IProductQueryService queryService)
    : IRequestHandler<GetByIdProductQuery, Result<GetByIdProductResponse>>
{
    public async Task<Result<GetByIdProductResponse>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
    {
        var product = await queryService.GetByIdAsync(request.Id, cancellationToken);
        return product is null ? ProductErrors.NotFound : product;
    }
}


//public class GetByIdProductQuery(IProductQueryService productQueryService)
//{
//    public async Task<Result<GetByIdProductResponse>> ExecuteAsync(Guid id, CancellationToken ct = default)
//    {
//        var product = await productQueryService.GetByIdAsync(id, ct);
//        return product is null ? ProductErrors.NotFound : product;
//    }
//}
