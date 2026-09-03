using ECommerceApp.Application.Common.Services;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Persistence.Contexts;
using ECommerceApp.Infrastructure.Persistence.Seeding.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ECommerceApp.Infrastructure.Persistence.Seeding;

public static class JsonSeeder
{
    public const long MaxSize = 5 * 1024 * 1024;  // 5MB
    public static readonly HashSet<string> allowedExtensions =
        new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg",
        ".jpeg",
        ".png"
    };
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
        bool hasImages = false,
        ECommerceDbContext? dbContext = null,
        IImageService? imageService = null,
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

        if (hasImages && imageService is not null && dbContext is not null)
        {
            foreach (var model in models)
            {
                var uploadedPublicIds = new List<string>();

                if (model is ProductSeedModel product)
                {
                    try
                    {
                        var newProduct = Product.Create(product.Name, product.Description, product.Price, product.BrandId, product.TypeId);
                        if (newProduct.IsFailure) return;

                        foreach (var imagePath in product.Pictures)
                        {
                            var path = Path.Combine(_fileFolder, "Images", "Products", imagePath);
                            if (!File.Exists(filePath)) return;

                            var extension = Path.GetExtension(path);
                            if (string.IsNullOrWhiteSpace(extension) || !allowedExtensions.Contains(extension))
                                return;


                            await using var stream = File.OpenRead(path);
                            if (!stream.CanRead) return;
                            if (stream.Length > MaxSize) return;

                            var uploaded = await imageService.UploadPhotoAsync(
                                stream,
                                imagePath,
                                ct);

                            uploadedPublicIds.Add(uploaded.PublicId);
                            var images = new List<ProductImage>();

                            newProduct.Value.AddImage(uploaded.PublicId, uploaded.Url,
                                newProduct.Value.Images.Count == 0, newProduct.Value.Images.Count);
                        }

                        await dbContext.Products.AddAsync(newProduct.Value, ct);
                    }
                    catch
                    {
                        foreach (var publicId in uploadedPublicIds)
                        {
                            await imageService.DeletePhotoAsync(publicId, ct);
                        }
                        //throw;
                    }

                }
            }
        }
        else
        {
            await dbSet.AddRangeAsync(models.Select(map), ct);
        }
    }
}
