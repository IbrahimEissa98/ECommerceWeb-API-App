using ECommerceApp.Domain.Common;
using ECommerceApp.Domain.Errors;

namespace ECommerceApp.Domain.Entities;

public class Product : BaseEntity<Guid>
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string PictureUrl { get; private set; } = default!;
    public decimal Price { get; private set; }

    public int BrandId { get; private set; }
    public ProductBrand ProductBrand { get; private set; } = default!;

    public int TypeId { get; private set; }
    public ProductType ProductType { get; private set; } = default!;

    private const int NameMaxLength = 100;
    private const int DescriptionMaxLength = 500;
    private const int PictureUrlMaxLength = 200;

    private Product() { }
    //private Product(string name, string description, string pictureUrl,
    //                decimal price, int brandId, int typeId)
    //{
    //    SetId(Guid.NewGuid());
    //    SetName(name);
    //    SetDescription(description);
    //    SetPictureUrl(pictureUrl);
    //    SetPrice(price);
    //    SetBrandId(brandId);
    //    SetTypeId(typeId);
    //}

    public static Result<Product> Create(
        string name,
        string description,
        string pictureUrl,
        decimal price,
        int brandId,
        int typeId)
    {
        var product = new Product();

        product.SetId(Guid.NewGuid());

        var nameResult = product.SetName(name);
        if (nameResult.IsFailure)
            return Result<Product>.Failure(nameResult.Error!);

        var descriptionResult = product.SetDescription(description);
        if (descriptionResult.IsFailure)
            return Result<Product>.Failure(descriptionResult.Error!);

        var pictureUrlResult = product.SetPictureUrl(pictureUrl);
        if (pictureUrlResult.IsFailure)
            return Result<Product>.Failure(pictureUrlResult.Error!);

        var priceResult = product.SetPrice(price);
        if (priceResult.IsFailure)
            return Result<Product>.Failure(priceResult.Error!);

        var brandResult = product.SetBrandId(brandId);
        if (brandResult.IsFailure)
            return Result<Product>.Failure(brandResult.Error!);

        var typeResult = product.SetTypeId(typeId);
        if (typeResult.IsFailure)
            return Result<Product>.Failure(typeResult.Error!);

        return Result<Product>.Success(product);
    }

    public Result Update(
        string name,
        string description,
        string pictureUrl,
        decimal price,
        int brandId,
        int typeId)
    {
        var nameResult = SetName(name);
        if (nameResult.IsFailure)
            return nameResult;

        var descriptionResult = SetDescription(description);
        if (descriptionResult.IsFailure)
            return descriptionResult;

        var pictureUrlResult = SetPictureUrl(pictureUrl);
        if (pictureUrlResult.IsFailure)
            return pictureUrlResult;

        var priceResult = SetPrice(price);
        if (priceResult.IsFailure)
            return priceResult;

        var brandResult = SetBrandId(brandId);
        if (brandResult.IsFailure)
            return brandResult;

        return SetTypeId(typeId);
    }

    public Result ChangePrice(decimal newPrice)
    {
        return SetPrice(newPrice);
    }

    public Result ChangePicture(string pictureUrl)
    {
        return SetPictureUrl(pictureUrl);
    }

    public Result ChangeBrand(int brandId)
    {
        return SetBrandId(brandId);
    }

    public Result ChangeType(int typeId)
    {
        return SetTypeId(typeId);
    }

    private Result SetName(string name)
    {
        var result = ValidateName(name);

        if (result.IsFailure)
            return result;

        Name = name.Trim();

        return Result.Success();
    }

    private Result SetDescription(string description)
    {
        var result = ValidateDescription(description);

        if (result.IsFailure)
            return result;

        Description = description.Trim();

        return Result.Success();
    }

    private Result SetPictureUrl(string pictureUrl)
    {
        var result = ValidatePictureUrl(pictureUrl);

        if (result.IsFailure)
            return result;

        PictureUrl = pictureUrl.Trim();

        return Result.Success();
    }

    private Result SetPrice(decimal price)
    {
        var result = ValidatePrice(price);

        if (result.IsFailure)
            return result;

        Price = decimal.Round(
            price,
            2,
            MidpointRounding.AwayFromZero);

        return Result.Success();
    }

    private Result SetBrandId(int brandId)
    {
        var result = ValidateBrandId(brandId);

        if (result.IsFailure)
            return result;

        BrandId = brandId;

        return Result.Success();
    }

    private Result SetTypeId(int typeId)
    {
        var result = ValidateTypeId(typeId);

        if (result.IsFailure)
            return result;

        TypeId = typeId;

        return Result.Success();
    }

    private static Result ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure(ProductErrors.NameRequired);

        if (name.Trim().Length > NameMaxLength)
            return Result.Failure(ProductErrors.NameTooLong);

        return Result.Success();
    }

    private static Result ValidateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure(ProductErrors.DescriptionRequired);

        if (description.Trim().Length > DescriptionMaxLength)
            return Result.Failure(ProductErrors.DescriptionTooLong);

        return Result.Success();
    }

    private static Result ValidatePictureUrl(string pictureUrl)
    {
        if (string.IsNullOrWhiteSpace(pictureUrl))
            return Result.Failure(ProductErrors.PictureUrlRequired);

        if (pictureUrl.Trim().Length > PictureUrlMaxLength)
            return Result.Failure(ProductErrors.PictureUrlTooLong);

        return Result.Success();
    }

    private static Result ValidatePrice(decimal price)
    {
        if (price < 0)
            return Result.Failure(ProductErrors.InvalidPrice);

        return Result.Success();
    }

    private static Result ValidateBrandId(int brandId)
    {
        if (brandId <= 0)
            return Result.Failure(ProductErrors.BrandNotFound);

        return Result.Success();
    }

    private static Result ValidateTypeId(int typeId)
    {
        if (typeId <= 0)
            return Result.Failure(ProductErrors.TypeNotFound);

        return Result.Success();
    }
}
