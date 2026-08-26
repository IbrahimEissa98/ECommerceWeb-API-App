using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Repositories;
using ECommerceApp.Infrastructure.Persistence.Contexts;
using System.Collections.Concurrent;

namespace ECommerceApp.Infrastructure.Repositories;

public class UnitOfWork(ECommerceDbContext dbContext) : IUnitOfWork
{
    private readonly ECommerceDbContext _dbContext = dbContext;
    private readonly ConcurrentDictionary<Type, object> _repositories = new();

    public IRepository<TEntity, TKey> Repository<TEntity, TKey>()
        where TEntity : BaseEntity<TKey>
        where TKey : struct
    {
        var type = typeof(TEntity);
        if (_repositories.TryGetValue(type, out var repo))
            return (IRepository<TEntity, TKey>)repo;

        var newRepo = new Repository<TEntity, TKey>(_dbContext);
        _repositories.TryAdd(type, newRepo);
        return newRepo;
    }

    public async Task<int> CommitAsync(CancellationToken ct = default)
        => await _dbContext.SaveChangesAsync(ct);
}
