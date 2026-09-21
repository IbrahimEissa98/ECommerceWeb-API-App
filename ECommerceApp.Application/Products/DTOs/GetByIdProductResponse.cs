namespace ECommerceApp.Application.Products.DTOs;

public record GetByIdProductResponse
        (Guid Id,
    string Name,
    string Description,
    string[] PicturesUrl,
    decimal Price,
    string Brand,
    string Type);
