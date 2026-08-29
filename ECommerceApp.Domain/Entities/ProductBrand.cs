using ECommerceApp.Domain.Common;

namespace ECommerceApp.Domain.Entities;

public class ProductBrand : BaseEntity<int>
{
    public string Name { get; private set; } = default!;

    public ICollection<Product> Products { get; private set; } = [];

    private ProductBrand() { }

    public static Result<ProductBrand> Create(string name)
    {
        var productBrand = new ProductBrand();

        var nameResult = productBrand.SetName(name);
        if (nameResult.IsFailure)
            return Result<ProductBrand>.Failure(nameResult.Error!);

        return Result<ProductBrand>.Success(productBrand);
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
            return Result.Failure(Error.Validation("ProductBrand.Name.Required",
                "Product brand name is required"));

        if (name.Trim().Length > 100)
            return Result.Failure(
                Error.Validation("ProductBrand.Name.TooLong",
                "Product brand name cannot exceed 100 characters."));

        return Result.Success();
    }
}
