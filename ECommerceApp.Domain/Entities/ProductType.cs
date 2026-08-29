using ECommerceApp.Domain.Common;

namespace ECommerceApp.Domain.Entities;

public class ProductType : BaseEntity<int>
{
    public string Name { get; private set; } = default!;

    public ICollection<Product> Products { get; private set; } = [];

    private ProductType() { }

    public static Result<ProductType> Create(string name)
    {
        var productType = new ProductType();

        var nameResult = productType.SetName(name);
        if (nameResult.IsFailure)
            return Result<ProductType>.Failure(nameResult.Error!);

        return Result<ProductType>.Success(productType);
    }

    private Result SetName(string name)
    {
        var result = ValidateName(name);

        if (result.IsFailure)
            return result;

        Name = name.Trim();

        return Result.Success();
    }

    private static Result ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure(Error.Validation("ProductType.Name.Required",
                "Product type name is required"));

        if (name.Trim().Length > 100)
            return Result.Failure(
                Error.Validation("ProductType.Name.TooLong",
                "Product type name cannot exceed 100 characters."));

        return Result.Success();
    }
}
