using ECommerceApp.Application.Messaging.Abstractions;
using ECommerceApp.Application.Products.DTOs;
using ECommerceApp.Application.Products.Enums;
using ECommerceApp.Application.Products.Specifications;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Repositories;

namespace ECommerceApp.Application.Products.Queries.GetPagedProducts;

public sealed record GetPagedProductsQuery(
    int PageNumber = 1,
    int PageSize = 5,
    string? Search = null,
    int? BrandId = null,
    int? TypeId = null,
    ProductSortField? SortBy = ProductSortField.Name,
    bool SortAscending = true
    ) : IQuery<Result<PagedResult<GetAllProductsResponse>>>;

public class GetPagedProductsHandler(IReadRepository<Product, Guid> repo)
    : IRequestHandler<GetPagedProductsQuery, Result<PagedResult<GetAllProductsResponse>>>
{
    public async Task<Result<PagedResult<GetAllProductsResponse>>> Handle
        (GetPagedProductsQuery request, CancellationToken cancellationToken)
    {
        var countSpec = new ProductsPagedSpec(request.Search, request.BrandId, request.TypeId);
        var listSpec = new ProductsPagedSpec(request.Search, request.BrandId, request.TypeId,
            request.SortBy, request.SortAscending, request.PageNumber, request.PageSize);

        var products = await repo.ToListAsync<GetAllProductsResponse>(listSpec, cancellationToken);
        var count = await repo.CountAsync(countSpec, cancellationToken);

        return Result<PagedResult<GetAllProductsResponse>>.Success(new PagedResult<GetAllProductsResponse>(products, count));
    }
}