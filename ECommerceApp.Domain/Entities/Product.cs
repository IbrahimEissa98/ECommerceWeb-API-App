using ECommerceApp.Domain.Common;

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
    private Product(string name, string description, string pictureUrl,
                    decimal price, int brandId, int typeId)
    {
        SetId(Guid.NewGuid());
        SetName(name);
        SetDescription(description);
        SetPictureUrl(pictureUrl);
        SetPrice(price);
        SetBrandId(brandId);
        SetTypeId(typeId);
    }

    public static Product Create(
        string name,
        string description,
        string pictureUrl,
        decimal price,
        int brandId,
        int typeId)
    {
        return new Product(
            name,
            description,
            pictureUrl,
            price,
            brandId,
            typeId);
    }

    //public void Update(
    //    string name,
    //    string description,
    //    string pictureUrl,
    //    decimal price,
    //    int brandId,
    //    int typeId)
    //{
    //    SetName(name);
    //    SetDescription(description);
    //    SetPictureUrl(pictureUrl);
    //    SetPrice(price);
    //    SetBrandId(brandId);
    //    SetTypeId(typeId);
    //}

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty.", nameof(name));

        if (name.Length > NameMaxLength)
            throw new ArgumentException("Product name cannot exceed 000 characters.", nameof(name));

        Name = name.Trim();
    }

    private void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Product description cannot be empty.", nameof(description));

        if (description.Length > DescriptionMaxLength)
            throw new ArgumentException(
                "Product description cannot exceed 500 characters.",
                nameof(description));

        Description = description.Trim();
    }

    private void SetPictureUrl(string pictureUrl)
    {
        if (string.IsNullOrWhiteSpace(pictureUrl))
            throw new ArgumentException("Product picture URL cannot be empty.", nameof(pictureUrl));

        if (pictureUrl.Length > PictureUrlMaxLength)
            throw new ArgumentException(
                "Product picture URL cannot exceed 200 characters.",
                nameof(pictureUrl));

        PictureUrl = pictureUrl.Trim();
    }

    private void SetPrice(decimal price)
    {
        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Product price cannot be negative.");

        Price = decimal.Round(price, 2, MidpointRounding.AwayFromZero);
    }

    private void SetBrandId(int brandId)
    {
        if (brandId <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(brandId),
                "Brand ID must be greater than zero.");

        BrandId = brandId;
    }

    private void SetTypeId(int typeId)
    {
        if (typeId <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(typeId),
                "Type ID must be greater than zero.");

        TypeId = typeId;
    }

    //public void ChangePrice(decimal newPrice)
    //{
    //    SetPrice(newPrice);
    //}

    //public void ChangePicture(string pictureUrl)
    //{
    //    SetPictureUrl(pictureUrl);
    //}

    //public void ChangeBrand(int brandId)
    //{
    //    SetBrandId(brandId);
    //}

    //public void ChangeType(int typeId)
    //{
    //    SetTypeId(typeId);
    //}
}
