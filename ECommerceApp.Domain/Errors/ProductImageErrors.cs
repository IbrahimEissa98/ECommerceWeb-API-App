using ECommerceApp.Domain.Common;

namespace ECommerceApp.Domain.Errors;

public static class ProductImageErrors
{
    public static readonly Error InvalidProductId =
        Error.Validation("ProductImage.InvalidProductId", "Product ID cannot be empty.");

    public static readonly Error InvalidPublicId =
        Error.Validation("ProductImage.InvalidPublicId", "Cloudinary public ID cannot be empty.");

    public static readonly Error InvalidUrl =
        Error.Validation("ProductImage.InvalidUrl", "Cloudinary secure url cannot be empty.");

    public static readonly Error InvalidDisplayOrder =
        Error.Validation("ProductImage.InvalidDisplayOrder", "Display order cannot be negative.");
}