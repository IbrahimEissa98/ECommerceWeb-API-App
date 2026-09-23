using Asp.Versioning;
using ECommerceApp.API.Common.Extensions;
using ECommerceApp.API.Common.Responses;
using ECommerceApp.Application.Messaging.Abstractions;
using ECommerceApp.Application.Products.DTOs;
using ECommerceApp.Application.Products.Queries.GetAllProducts;
using ECommerceApp.Application.Products.Queries.GetPagedProducts;
using ECommerceApp.Application.Products.Queries.GetProductById;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Swashbuckle.AspNetCore.Annotations;

namespace ECommerceApp.API.Controllers;

public class ProductsController(ISender sender) : ApiBaseController
{
    [HttpGet]
    [OutputCache(Duration = 30, Tags = ["Products"])]
    [SwaggerResponse(200, "Products found", typeof(ApiResponse<GetAllProductsResponse>))]
    [ApiVersion(1, Deprecated = true)]
    public async Task<ActionResult<IReadOnlyList<GetAllProductsResponse>>> ListAll(CancellationToken ct)
    {
        var result = await sender.Send(new GetAllProductsQuery(), ct);
        //return Ok(result.Value);
        return result.ToApiResponse(HttpContext);
    }

    [HttpGet("{id}")]
    [SwaggerResponse(200, "Product found", typeof(GetByIdProductResponse))]
    [SwaggerResponse(404, "Product not found", typeof(ProblemApiResponse))]
    [SwaggerResponse(400, "Invalid id", typeof(ProblemDetails))]
    [ApiVersion(1, Deprecated = true)]
    public async Task<ActionResult<GetByIdProductResponse>> GetById(Guid id, CancellationToken ct)
    {
        var result = await sender.Send(new GetByIdProductQuery(id), ct);
        return result.ToApiResponse(HttpContext);

        //return result.Match<ActionResult<GetByIdProductResponse>>(
        //    product => Ok(product),
        //    error => NotFound(error.Message));
    }

    //[HttpGet]
    //[SwaggerResponse(200, "Products found", typeof(ApiResponse<GetAllProductsResponse>))]
    //[ApiVersion(2)]
    //public async Task<ActionResult<IReadOnlyList<GetAllProductsResponse>>> ListAll_V2(CancellationToken ct)
    //{
    //    var result = await sender.Send(new GetAllProductsQueryV2(), ct);
    //    return result.ToApiResponse(HttpContext);
    //}

    [HttpGet]
    [OutputCache(Duration = 30, Tags = ["Products"], PolicyName = "Products")]
    [SwaggerResponse(200, "Products found", typeof(ApiResponse<GetAllProductsResponse>))]
    [ApiVersion(2)]
    public async Task<ActionResult<IReadOnlyList<GetAllProductsResponse>>> ListAllPaged_V2(
        [FromQuery] GetPagedProductsQuery query, CancellationToken ct)
    {
        var result = await sender.Send(query, ct);
        return result.ToApiResponse(HttpContext, new PaginationMeta(query.PageNumber, query.PageSize, result.Value.TotalCount));
    }

    [HttpGet("{id}")]
    [SwaggerResponse(200, "Product found", typeof(GetByIdProductResponse))]
    [SwaggerResponse(404, "Product not found", typeof(ProblemApiResponse))]
    [SwaggerResponse(400, "Invalid id", typeof(ProblemDetails))]
    [ApiVersion(2)]
    public async Task<ActionResult<GetByIdProductResponse>> GetById_V2(Guid id, CancellationToken ct)
    {
        var result = await sender.Send(new GetByIdProductQueryV2(id), ct);
        return result.ToApiResponse(HttpContext);
    }
}
