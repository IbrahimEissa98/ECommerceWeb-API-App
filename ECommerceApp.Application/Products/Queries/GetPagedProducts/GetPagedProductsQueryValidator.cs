using FluentValidation;

namespace ECommerceApp.Application.Products.Queries.GetPagedProducts;

public class GetPagedProductsQueryValidator : AbstractValidator<GetPagedProductsQuery>
{
    private const int _minPageNumber = 1;
    private const int _minPageSize = 5;
    private const int _maxPageSize = 30;
    private const int _minBrandId = 1;
    private const int _minTypeId = 1;

    public GetPagedProductsQueryValidator()
    {
        RuleFor(p => p.PageNumber)
            .NotNull()
            .GreaterThanOrEqualTo(_minPageNumber)
            .WithErrorCode("Products.PageNumber.Invalid")
            .WithMessage($"Page Number must be at least {_minPageNumber}");

        RuleFor(p => p.PageSize)
            .NotNull()
            .InclusiveBetween(_minPageSize, _maxPageSize)
            .WithErrorCode("Products.PageSize.Invalid")
            .WithMessage($"Page size must be between {_minPageSize} and {_maxPageSize}");

        RuleFor(p => p.BrandId)
            .GreaterThanOrEqualTo(_minBrandId)
            .WithErrorCode("Products.BrandId.Invalid")
            .WithMessage($"Brand id must be start from {_minBrandId}");

        RuleFor(p => p.TypeId)
            .GreaterThanOrEqualTo(_minTypeId)
            .WithErrorCode("Products.TypeId.Invalid")
            .WithMessage($"Type id must be start from {_minTypeId}");

        RuleFor(p => p.SortBy)
            .IsInEnum();
    }
}
