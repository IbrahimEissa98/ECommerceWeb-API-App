using ECommerceApp.Domain.Common;

namespace ECommerceApp.Domain.Repositories;

public interface IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : struct
{
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default);

    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default);

    Task AddAsync(TEntity entity, CancellationToken ct = default);

    void Update(TEntity entity);

    void Delete(TEntity entity);
}
