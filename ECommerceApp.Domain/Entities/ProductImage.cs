using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Errors;

namespace ECommerceApp.Domain.Entities;

public class ProductImage : BaseEntity<Guid>
{
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = default!;
    public string PublicId { get; private set; } = default!;
    public string Url { get; private set; } = default!;
    public bool IsPrimary { get; private set; }
    public int DisplayOrder { get; private set; }

    private ProductImage()
    {
    }

    private ProductImage(
        Guid productId,
        string publicId,
        string url,
        bool isPrimary,
        int displayOrder)
    {
        SetId(Guid.NewGuid());
        ProductId = productId;
        PublicId = publicId;
        Url = url;
        IsPrimary = isPrimary;
        DisplayOrder = displayOrder;
    }

    public static Result<ProductImage> Create(
        Guid productId,
        string publicId,
        string url,
        bool isPrimary = false,
        int displayOrder = 0)
    {
        if (productId == Guid.Empty)
            return ProductImageErrors.InvalidProductId;

        if (string.IsNullOrWhiteSpace(publicId))
            return ProductImageErrors.InvalidPublicId;

        if (string.IsNullOrWhiteSpace(url))
            return ProductImageErrors.InvalidPublicId;

        if (displayOrder < 0)
            return ProductImageErrors.InvalidDisplayOrder;

        var image = new ProductImage(
            productId,
            publicId.Trim(),
            url.Trim(),
            isPrimary,
            displayOrder);

        return image;
    }

    public Result SetAsPrimary()
    {
        IsPrimary = true;

        return Result.Success();
    }

    public Result RemovePrimary()
    {
        IsPrimary = false;

        return Result.Success();
    }

    public Result ChangeDisplayOrder(int displayOrder)
    {
        if (displayOrder < 0)
            return Result.Failure(
                ProductImageErrors.InvalidDisplayOrder);

        DisplayOrder = displayOrder;

        return Result.Success();
    }

    public Result ChangePublicId(string publicId)
    {
        if (string.IsNullOrWhiteSpace(publicId))
            return Result.Failure(
                ProductImageErrors.InvalidPublicId);

        PublicId = publicId.Trim();

        return Result.Success();
    }
}