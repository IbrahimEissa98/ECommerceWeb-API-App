namespace ECommerceApp.Application.Products.DTOs;

public record GetAllProductsResponse
    (Guid Id,
    string Name,
    string Description,
    string[] PicturesUrl,
    decimal Price,
    string Brand,
    string Type);
