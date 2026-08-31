using ECommerceApp.Application.Products.DTOs;
using ECommerceApp.Domain.Common;
using MediatR;

namespace ECommerceApp.Application.Products.Queries;

public record GetAllProductsQuery() : IRequest<Result<IReadOnlyList<GetAllProductsResponse>>>;

public class GetAllProductsHandler(IProductQueryService productQueryService)
    : IRequestHandler<GetAllProductsQuery, Result<IReadOnlyList<GetAllProductsResponse>>>
{
    public async Task<Result<IReadOnlyList<GetAllProductsResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productQueryService.GetAllAsync(cancellationToken);
        return Result<IReadOnlyList<GetAllProductsResponse>>.Success(products);
    }
}


//public sealed class GetAllProductQuery(IProductQueryService productQueryService)
//{
//    private readonly IProductQueryService _productQueryService = productQueryService;

//    public async Task<Result<IReadOnlyList<GetAllProductsResponse>>> ExecuteAsync(CancellationToken ct = default)
//    {
//        var products = await _productQueryService.GetAllAsync(ct);
//        return Result<IReadOnlyList<GetAllProductsResponse>>.Success(products);
//    }
//}