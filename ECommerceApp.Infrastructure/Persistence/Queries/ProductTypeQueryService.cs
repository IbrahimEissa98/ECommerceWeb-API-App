using ECommerceApp.Application.ProductTypes;
using ECommerceApp.Application.ProductTypes.DTOs;
using ECommerceApp.Infrastructure.Persistence.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Infrastructure.Persistence.Queries;

public class ProductTypeQueryService(ECommerceDbContext dbContext) : IProductTypeQueryService
{
    async Task<IReadOnlyList<GetAllProductTypesResponse>> IProductTypeQueryService.GetAllAsync(CancellationToken ct)
    {
        return [..await dbContext.Types
                    .ProjectToType<GetAllProductTypesResponse>()
                    .ToListAsync(ct)];
    }

    async Task<GetByIdProductTypeResponse?> IProductTypeQueryService.GetByIdAsync(int id, CancellationToken ct)
    {
        return await dbContext.Types
                    .ProjectToType<GetByIdProductTypeResponse>()
                    .FirstOrDefaultAsync(t => t.Id == id, ct);
    }
}
