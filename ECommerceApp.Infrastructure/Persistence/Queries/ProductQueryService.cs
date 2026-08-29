using ECommerceApp.Application.Products;
using ECommerceApp.Application.Products.DTOs;
using ECommerceApp.Infrastructure.Persistence.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Infrastructure.Persistence.Queries;

public class ProductQueryService(ECommerceDbContext dbContext) : IProductQueryService
{
    private readonly ECommerceDbContext _dbContext = dbContext;

    public async Task<IReadOnlyList<GetAllProductsResponse>> GetAllAsync(CancellationToken ct = default)
    {
        return [..await _dbContext.Products
                    .ProjectToType<GetAllProductsResponse>()
                    .ToListAsync(ct)];
    }

    public async Task<GetByIdProductResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbContext.Products
                    .ProjectToType<GetByIdProductResponse>()
                    .FirstOrDefaultAsync(p => p.Id == id, ct);
    }
}
