namespace ECommerceApp.Infrastructure.Persistence.Seeding;

public interface IDataSeeder
{
    public int Order { get; }

    Task SeedAsync(CancellationToken ct = default);
}
