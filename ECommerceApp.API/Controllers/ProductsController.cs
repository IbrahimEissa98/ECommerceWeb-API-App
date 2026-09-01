using ECommerceApp.API.Common.Extensions;
using ECommerceApp.API.Common.Responses;
using ECommerceApp.Application.Products.DTOs;
using ECommerceApp.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ECommerceApp.API.Controllers;

public class ProductsController(ISender mediatr) : ApiBaseController
{
    [HttpGet]
    [SwaggerResponse(200, "Products found", typeof(ApiResponse<GetAllProductsResponse>))]
    public async Task<ActionResult<IReadOnlyList<GetAllProductsResponse>>> ListAll(CancellationToken ct)
    {
        var result = await mediatr.Send(new GetAllProductsQuery(), ct);
        //return Ok(result.Value);
        return result.ToApiResponse(HttpContext);
    }

    [HttpGet("{id}")]
    [SwaggerResponse(200, "Product found", typeof(GetByIdProductResponse))]
    [SwaggerResponse(404, "Product not found", typeof(ProblemApiResponse))]
    [SwaggerResponse(400, "Invalid id", typeof(ProblemDetails))]
    public async Task<ActionResult<GetByIdProductResponse>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediatr.Send(new GetByIdProductQuery(id), ct);
        return result.ToApiResponse(HttpContext);

        //return result.Match<ActionResult<GetByIdProductResponse>>(
        //    product => Ok(product),
        //    error => NotFound(error.Message));
    }
}
