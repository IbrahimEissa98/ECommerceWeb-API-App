using ECommerceApp.Domain.Common;

namespace ECommerceApp.Domain.Errors;

public static class ProductErrors
{
    public static readonly Error NotFound =
        Error.NotFound("Product.NotFound", "Product was not found.");

    public static readonly Error NameRequired =
        Error.Validation("Product.Name.Required", "Product name is required.");

    public static readonly Error NameTooLong =
        Error.Validation("Product.Name.TooLong", "Product name cannot exceed 200 characters.");

    public static readonly Error DescriptionRequired =
        Error.Validation("Product.Description.Required", "Product description is required.");

    public static readonly Error DescriptionTooLong =
        Error.Validation("Product.Description.TooLong", "Product description cannot exceed 5000 characters.");

    public static readonly Error PictureUrlRequired =
        Error.Validation("Product.PictureUrl.Required", "Product picture URL is required.");

    public static readonly Error PictureUrlTooLong =
        Error.Validation("Product.PictureUrl.TooLong", "Product picture URL cannot exceed 2048 characters.");

    public static readonly Error InvalidPrice =
        Error.Validation("Product.Price.Invalid", "Product price cannot be negative.");

    public static readonly Error BrandNotFound =
        Error.NotFound("Product.Brand.NotFound", "Product brand was not found.");

    public static readonly Error TypeNotFound =
        Error.NotFound("Product.Type.NotFound", "Product type was not found.");
}
