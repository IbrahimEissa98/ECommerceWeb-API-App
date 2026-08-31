using ECommerceApp.Application.Products.DTOs;
using ECommerceApp.Application.Products.Queries;
using MediatR;

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
            async (ISender mediatr, CancellationToken ct) =>
            {
                var products = await mediatr.Send(new GetAllProductsQuery(), ct);
                return products.IsSuccess ? Results.Ok(products.Value) : Results.NotFound(products.Error);
            })
            .WithName("GetAllProducts")
            .Produces<IReadOnlyList<GetAllProductsResponse>>(StatusCodes.Status200OK);

        group.MapGet("/products/{id:guid}",
            async (Guid id, ISender mediatr, CancellationToken ct) =>
            {
                var product = await mediatr.Send(new GetByIdProductQuery(id), ct);
                return product.IsSuccess ? Results.Ok(product.Value) : Results.NotFound(product.Error);
            })
            .WithName("GetProductById")
            .Produces<GetByIdProductResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        return endpoint;


        //group.MapGet("/products",
        //    async (GetAllProductQuery getAll, CancellationToken ct) =>
        //    {
        //        var products = await getAll.ExecuteAsync(ct);
        //        return products.IsSuccess ? Results.Ok(products.Value) : Results.NotFound(products.Error);
        //    })
        //    .WithName("GetAllProducts")
        //    .Produces<IReadOnlyList<GetAllProductsResponse>>(StatusCodes.Status200OK);

        //group.MapGet("/products/{id:guid}",
        //    async (Guid id, GetByIdProductQuery getById, CancellationToken ct) =>
        //    {
        //        var product = await getById.ExecuteAsync(id, ct);
        //        return product.IsSuccess ? Results.Ok(product.Value) : Results.NotFound(product.Error);
        //    })
        //    .WithName("GetProductById")
        //    .Produces<GetByIdProductResponse>(StatusCodes.Status200OK)
        //    .Produces(StatusCodes.Status404NotFound);

        //return endpoint;
    }
}
