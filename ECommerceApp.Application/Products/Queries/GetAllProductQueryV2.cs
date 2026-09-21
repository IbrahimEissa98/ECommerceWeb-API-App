using ECommerceApp.Application.Products.DTOs;
using ECommerceApp.Application.Products.Specifications;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Repositories;
using MediatR;

namespace ECommerceApp.Application.Products.Queries;

public record GetAllProductsQueryV2() : IRequest<Result<IReadOnlyList<GetAllProductsResponse>>>;

public class GetAllProductsHandlerV2(IReadRepository<Product, Guid> repo)
    : IRequestHandler<GetAllProductsQueryV2, Result<IReadOnlyList<GetAllProductsResponse>>>
{
    public async Task<Result<IReadOnlyList<GetAllProductsResponse>>> Handle(GetAllProductsQueryV2 request, CancellationToken cancellationToken)
    {
        var products = await repo.ToListAsync<GetAllProductsResponse>(new ProductListSpec(), cancellationToken);
        return Result<IReadOnlyList<GetAllProductsResponse>>.Success(products);
    }
}