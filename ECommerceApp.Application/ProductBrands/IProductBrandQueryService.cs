using ECommerceApp.Application.ProductBrands.DTOs;
using ECommerceApp.Application.ProductTypes.DTOs;

namespace ECommerceApp.Application.ProductBrands;

public interface IProductBrandQueryService
{
    Task<IReadOnlyList<GetAllProductBrandsResponse>> GetAllAsync(CancellationToken ct = default);
    Task<GetByIdProductBrandResponse?> GetByIdAsync(int id, CancellationToken ct = default);
}
