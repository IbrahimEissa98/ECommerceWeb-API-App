using ECommerceApp.API.Common.Extensions;
using ECommerceApp.API.Common.Responses;
using ECommerceApp.Application.ProductTypes.DTOs;
using ECommerceApp.Application.ProductTypes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ECommerceApp.API.Controllers;

public class TypesController(ISender mediatr) : ApiBaseController
{
    [HttpGet]
    [SwaggerResponse(200, "Types found", typeof(ApiResponse<GetAllProductTypesResponse>))]
    public async Task<ActionResult<IReadOnlyList<GetAllProductTypesResponse>>> ListAll(CancellationToken ct)
    {
        var result = await mediatr.Send(new GetAllProductTypesQuery(), ct);
        return result.ToApiResponse(HttpContext);
    }

    [HttpGet("{id}")]
    [SwaggerResponse(200, "Type found", typeof(ApiResponse<GetByIdProductTypeResponse>))]
    [SwaggerResponse(404, "Type not found", typeof(ProblemApiResponse))]
    [SwaggerResponse(400, "Invalid id", typeof(ProblemDetails))]
    public async Task<ActionResult<GetByIdProductTypeResponse>> ListAll(int id, CancellationToken ct)
    {
        var result = await mediatr.Send(new GetByIdProductTypeQuery(id), ct);
        return result.ToApiResponse(HttpContext);
    }
}
