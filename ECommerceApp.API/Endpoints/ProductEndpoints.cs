using ECommerceApp.Application.Products.DTOs;
using ECommerceApp.Application.Products.Queries;

namespace ECommerceApp.API.Endpoints;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder endpoint)
    {
        var products = endpoint.NewVersionedApi("Products");

        var group = products.MapGroup("/api/v{version:apiVersion}")
            .HasApiVersion(1.0)
            .WithTags("Products");

        group.MapGet("/products",
            async (GetAllProductQuery getAll, CancellationToken ct) =>
            {
                var products = await getAll.ExecuteAsync(ct);
                return products.IsSuccess ? Results.Ok(products.Value) : Results.NotFound(products.Error);
            })
            .WithName("GetAllProducts")
            .Produces<IReadOnlyList<GetAllProductsResponse>>(StatusCodes.Status200OK);

        group.MapGet("/products/{id:guid}",
            async (Guid id, GetByIdProductQuery getById, CancellationToken ct) =>
            {
                var product = await getById.ExecuteAsync(id, ct);
                return product.IsSuccess ? Results.Ok(product.Value) : Results.NotFound(product.Error);
            })
            .WithName("GetProductById")
            .Produces<GetByIdProductResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        return endpoint;
    }


    //public static IEndpointRouteBuilder MapProductV2Endpoints(this IEndpointRouteBuilder endpoint)
    //{
    //    var productsV2 = endpoint.NewVersionedApi("ProductsV2");
    //    var group = productsV2.MapGroup("/api/v{version:apiVersion}")
    //        .HasApiVersion(2.0)
    //        .WithTags("ProductsV2");

    //    group.MapGet("/brands",
    //        async (GetAllProductBrandsQuery getAll, CancellationToken ct) =>
    //        {
    //            var brands = await getAll.ExecuteAsync(ct);
    //            return brands.IsSuccess ? Results.Ok(brands.Value) : Results.NotFound(brands.Error);
    //        })
    //        .WithName("GetAllProductBrands")
    //        .Produces<IReadOnlyList<GetAllProductBrandsResponse>>(StatusCodes.Status200OK);

    //    group.MapGet("/types",
    //        async (GetAllProductTypesQuery getAll, CancellationToken ct) =>
    //        {
    //            var types = await getAll.ExecuteAsync(ct);
    //            return types.IsSuccess ? Results.Ok(types.Value) : Results.NotFound();
    //        })
    //        .WithName("GetAllProductTypes")
    //        .Produces<GetAllProductTypesResponse>(StatusCodes.Status200OK)
    //        .Produces(StatusCodes.Status404NotFound);

    //    return endpoint;
    //}
}
