using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ECommerceApp.Application.Common.DTOs;
using ECommerceApp.Application.Common.Services;

namespace ECommerceApp.Infrastructure.Persistence.Storage;

public sealed class CloudinaryImageService(Cloudinary cloudinary) : IImageService
{
    public async Task<ImageUploadResponse> UploadPhotoAsync(Stream stream, string fileName, CancellationToken ct = default)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(fileName, stream),
            Folder = "ecommerce/products",
            UseFilename = false,
            UniqueFilename = true
        };

        var result = await cloudinary.UploadAsync(uploadParams, ct);

        if (result.Error is not null)
        {
            throw new InvalidOperationException($"Cloudinary upload failed: {result.Error.Message}");
        }
        return new ImageUploadResponse(result.PublicId, result.SecureUrl.ToString());
    }

    public async Task DeletePhotoAsync(string publicId, CancellationToken ct = default)
    {
        var deleteParams = new DeletionParams(publicId);

        var result = await cloudinary.DestroyAsync(deleteParams);

        if (result.Result != "ok")
        {
            throw new InvalidOperationException(
                $"Cloudinary deletion failed for '{publicId}'.");
        }
    }
}