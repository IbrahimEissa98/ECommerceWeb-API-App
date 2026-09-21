using ECommerceApp.Application.Messaging.Abstractions;
using ECommerceApp.Application.Products.DTOs;
using ECommerceApp.Application.Products.Specifications;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Errors;
using ECommerceApp.Domain.Repositories;

namespace ECommerceApp.Application.Products.Queries;

public record GetByIdProductQueryV2(Guid Id) : IQuery<Result<GetByIdProductResponse>>;

public class GetByIdProductHandlerV2(IReadRepository<Product, Guid> repo)
    : IRequestHandler<GetByIdProductQueryV2, Result<GetByIdProductResponse>>
{
    public async Task<Result<GetByIdProductResponse>> Handle(GetByIdProductQueryV2 request, CancellationToken cancellationToken)
    {
        var product = await repo.FirstOrDefaultAsync<GetByIdProductResponse>(new ProductByIdSpec(request.Id), cancellationToken);
        return product is null ? ProductErrors.NotFound : product;
    }
}