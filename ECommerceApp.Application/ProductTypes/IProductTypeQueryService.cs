using ECommerceApp.Application.ProductTypes.DTOs;

namespace ECommerceApp.Application.ProductTypes;

public interface IProductTypeQueryService
{
    Task<IReadOnlyList<GetAllProductTypesResponse>> GetAllAsync(CancellationToken ct = default);
    Task<GetByIdProductTypeResponse?> GetByIdAsync(int id, CancellationToken ct = default);
}
