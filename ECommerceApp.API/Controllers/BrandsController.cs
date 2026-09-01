using ECommerceApp.API.Common.Extensions;
using ECommerceApp.API.Common.Responses;
using ECommerceApp.Application.ProductBrands.DTOs;
using ECommerceApp.Application.ProductBrands.Queries;
using ECommerceApp.Application.Products.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ECommerceApp.API.Controllers;

public class BrandsController(ISender mediatr) : ApiBaseController
{
    [HttpGet]
    [SwaggerResponse(200, "Brands found", typeof(ApiResponse<GetAllProductsResponse>))]
    public async Task<ActionResult<IReadOnlyList<GetAllProductBrandsResponse>>> ListAll(CancellationToken ct)
    {
        var result = await mediatr.Send(new GetAllBrandsQuery(), ct);
        return result.ToApiResponse(HttpContext);
    }

    [HttpGet("{id}")]
    [SwaggerResponse(200, "Brand found", typeof(ApiResponse<GetByIdProductBrandResponse>))]
    [SwaggerResponse(404, "Brand not found", typeof(ProblemApiResponse))]
    [SwaggerResponse(400, "Invalid id", typeof(ProblemDetails))]
    public async Task<ActionResult<GetByIdProductBrandResponse>> GetById(int id, CancellationToken ct)
    {
        var result = await mediatr.Send(new GetByIdBrandQuery(id), ct);
        return result.ToApiResponse(HttpContext);
    }
}
