using ECommerceApp.Application.Common.DTOs;

namespace ECommerceApp.Application.Common.Services;

public interface IImageService
{
    Task<ImageUploadResponse> UploadPhotoAsync(Stream stream, string fileName, CancellationToken ct = default);
    Task DeletePhotoAsync(string publicId, CancellationToken ct = default);
}
