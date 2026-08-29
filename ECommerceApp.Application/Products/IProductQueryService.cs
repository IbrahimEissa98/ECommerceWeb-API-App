using ECommerceApp.Application.Products.DTOs;

namespace ECommerceApp.Application.Products;

public interface IProductQueryService
{
    Task<IReadOnlyList<GetAllProductsResponse>> GetAllAsync(CancellationToken ct = default);
    Task<GetByIdProductResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
}
