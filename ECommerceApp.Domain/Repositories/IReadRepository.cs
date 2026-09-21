using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Specifications;

namespace ECommerceApp.Domain.Repositories;

public interface IReadRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey> where TKey : struct
{
    Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken ct = default);
    Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken ct = default);

    Task<TEntity> SingleAsync(ISpecification<TEntity> specification, CancellationToken ct = default);
    Task<TResult> SingleAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken ct = default);

    Task<IReadOnlyList<TEntity>> ToListAsync(ISpecification<TEntity> specification, CancellationToken ct = default);
    Task<IReadOnlyList<TResult>> ToListAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken ct = default);

    Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken ct = default);

    Task<bool> AnyAsync(ISpecification<TEntity> specification, CancellationToken ct = default);
}
