using ECommerceApp.Application.ProductTypes.DTOs;
using ECommerceApp.Application.ProductTypes.Specifications;
using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Repositories;
using MediatR;

namespace ECommerceApp.Application.ProductTypes.Queries;

public record GetAllProductTypesQueryV2 : IRequest<Result<IReadOnlyList<GetAllProductTypesResponse>>>;

public class GetAllProductTypesQueryHandlerV2(IReadRepository<ProductType, int> repo)
    : IRequestHandler<GetAllProductTypesQueryV2, Result<IReadOnlyList<GetAllProductTypesResponse>>>
{
    public async Task<Result<IReadOnlyList<GetAllProductTypesResponse>>> Handle(GetAllProductTypesQueryV2 request, CancellationToken cancellationToken)
    {
        var types = await repo.ToListAsync<GetAllProductTypesResponse>(new ProductTypesListSpec(), cancellationToken);
        return Result<IReadOnlyList<GetAllProductTypesResponse>>.Success(types);
    }
}
