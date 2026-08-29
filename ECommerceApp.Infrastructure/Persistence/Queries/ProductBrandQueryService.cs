using ECommerceApp.Application.ProductBrands;
using ECommerceApp.Application.ProductBrands.DTOs;
using ECommerceApp.Application.ProductTypes;
using ECommerceApp.Application.ProductTypes.DTOs;
using ECommerceApp.Infrastructure.Persistence.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Infrastructure.Persistence.Queries;

public class ProductBrandQueryService(ECommerceDbContext dbContext) : IProductBrandQueryService
{
    public async Task<IReadOnlyList<GetAllProductBrandsResponse>> GetAllAsync(CancellationToken ct = default)
    {
        return [..await dbContext.Brands
                        .ProjectToType<GetAllProductBrandsResponse>()
                        .ToListAsync(ct)];
    }

    public async Task<GetByIdProductBrandResponse?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await dbContext.Brands
                        .ProjectToType<GetByIdProductBrandResponse>()
                        .FirstOrDefaultAsync(b => b.Id == id, ct);
    }
}
