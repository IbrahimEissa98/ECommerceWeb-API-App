using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Repositories;
using ECommerceApp.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Infrastructure.Repositories;

public class Repository<TEntity, TKey>(ECommerceDbContext dbContext)
    : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : struct
{
    private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
        => [.. await _dbSet.AsNoTracking().ToListAsync(ct)];

    public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default)
        => await _dbSet.FindAsync([id], ct);

    public async Task AddAsync(TEntity entity, CancellationToken ct = default)
        => await _dbSet.AddAsync(entity, ct);

    public void Update(TEntity entity)
        => _dbSet.Update(entity);

    public void Delete(TEntity entity)
        => _dbSet.Remove(entity);
}
