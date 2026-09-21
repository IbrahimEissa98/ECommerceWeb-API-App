using ECommerceApp.Application.ProductBrands.DTOs;
using ECommerceApp.Application.ProductBrands.Specifications;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Repositories;
using MediatR;

namespace ECommerceApp.Application.ProductBrands.Queries;

public record GetAllBrandsQueryV2 : IRequest<Result<IReadOnlyList<GetAllProductBrandsResponse>>>;

public class GetAllBrandsQueryHandlerV2(IReadRepository<ProductBrand, int> repo)
    : IRequestHandler<GetAllBrandsQueryV2, Result<IReadOnlyList<GetAllProductBrandsResponse>>>
{
    public async Task<Result<IReadOnlyList<GetAllProductBrandsResponse>>> Handle(GetAllBrandsQueryV2 request, CancellationToken cancellationToken)
    {
        var brands = await repo.ToListAsync<GetAllProductBrandsResponse>(new BrandsListSpec(), cancellationToken);
        return Result<IReadOnlyList<GetAllProductBrandsResponse>>.Success(brands);
    }
}
