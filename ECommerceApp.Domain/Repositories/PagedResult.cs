namespace ECommerceApp.Domain.Repositories;

public sealed record PagedResult<T>(IReadOnlyList<T> Items, int TotalCount);
