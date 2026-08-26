using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ECommerceApp.Infrastructure.Persistence.Seeding;

public static class JsonSeeder
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    private static readonly string _fileFolder =
        Path.Combine(AppContext.BaseDirectory, "Persistence", "Seeding", "Data");

    public static async Task SeedIfEmptyAsync<TEntity, TModel>(
        DbSet<TEntity> dbSet,
        string fileName,
        Func<TModel, TEntity> map,
        CancellationToken ct = default
        ) where TEntity : class
    {
        if (await dbSet.AnyAsync(ct)) return;

        ArgumentNullException.ThrowIfNullOrWhiteSpace(fileName);
        var filePath = Path.Combine(_fileFolder, fileName);
        if (!File.Exists(filePath)) return;

        await using var fileStream = File.OpenRead(filePath);

        var models = await JsonSerializer.DeserializeAsync<List<TModel>>(fileStream, _jsonOptions, ct);
        if (models is null || models.Count == 0) return;
        await dbSet.AddRangeAsync(models.Select(map), ct);
    }
}
