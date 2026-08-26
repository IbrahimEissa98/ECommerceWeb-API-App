using ECommerceApp.Domain.Common;

namespace ECommerceApp.Domain.Repositories;

public interface IUnitOfWork
{
    IRepository<TEntity, TKey> Repository<TEntity, TKey>()
        where TEntity : BaseEntity<TKey>
        where TKey : struct;

    Task<int> CommitAsync(CancellationToken ct = default);
}
