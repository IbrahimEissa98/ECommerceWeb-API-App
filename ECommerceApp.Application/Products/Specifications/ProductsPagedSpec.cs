using ECommerceApp.Application.Products.DTOs;
using ECommerceApp.Application.Products.Enums;
using ECommerceApp.Application.Specifications;
using ECommerceApp.Application.Specifications.Contracts;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.Products.Specifications;

public class ProductsPagedSpec : Specification<Product, GetAllProductsResponse>
{
    public ProductsPagedSpec(string? search = null, int? brandId = null,
        int? typeId = null, ProductSortField? sortBy = null, bool sortDescending = false, int? pageNumber = null,
        int? pageSize = null)
    {
        var query = Query;

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.ToLower().Trim();

            query.Where(p => p.Name.ToLower().Contains(term) || p.Description.ToLower().Contains(term));
        }

        if (brandId.HasValue)
            query.Where(p => p.BrandId == brandId.Value);

        if (typeId.HasValue)
            query = query.Where(p => p.TypeId == typeId.Value);

        if (sortBy is ProductSortField sortField)
            ApplySort(query, sortBy, sortDescending);

        if (pageNumber.HasValue && pageSize.HasValue)
        {
            var skip = (pageNumber - 1) * pageSize.Value;

            query
                .Skip(skip.Value)
                .Take(pageSize.Value)
                .Select(p => new GetAllProductsResponse(p.Id,
                    p.Name, p.Description, p.Images.Select(i => i.Url).ToArray(),
                    p.Price, p.ProductBrand.Name, p.ProductType.Name));
        }
    }

    private static void ApplySort(
    ISpecificationBuilder<Product, GetAllProductsResponse> query,
    ProductSortField? sortBy,
    bool sortDescending)
    {
        switch (sortBy)
        {
            case ProductSortField.Name:
                if (sortDescending)
                {
                    query.OrderByDescending(x => x.Name);
                }
                else
                {
                    query.OrderBy(x => x.Name);
                }
                break;

            case ProductSortField.Price:
                if (sortDescending)
                {
                    query.OrderByDescending(x => x.Price)
                         .ThenBy(x => x.Name);
                }
                else
                {
                    query.OrderBy(x => x.Price)
                         .ThenBy(x => x.Name);
                }
                break;

            case ProductSortField.Brand:
                if (sortDescending)
                {
                    query.OrderByDescending(x => x.ProductBrand.Name)
                         .ThenBy(x => x.Name);
                }
                else
                {
                    query.OrderBy(x => x.ProductBrand.Name)
                         .ThenBy(x => x.Name);
                }
                break;

            case ProductSortField.Type:
                if (sortDescending)
                {
                    query.OrderByDescending(x => x.ProductType.Name)
                         .ThenBy(x => x.Name);
                }
                else
                {
                    query.OrderBy(x => x.ProductType.Name)
                         .ThenBy(x => x.Name);
                }
                break;

            default:
                query.OrderBy(x => x.Name);
                break;
        }
    }
}
