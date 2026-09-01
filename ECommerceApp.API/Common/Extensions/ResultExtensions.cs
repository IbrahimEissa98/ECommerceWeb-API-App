using ECommerceApp.API.Common.Responses;
using ECommerceApp.Domain.Common;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.API.Common.Extensions;

public static class ResultExtensions
{
    public static ActionResult<T> ToApiResponse<T>(this Result<T> result, HttpContext httpContext, PaginationMeta? pagination = null)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(
                new ApiResponse<T>
                {
                    Success = true,
                    Data = result.Value,
                    MetaData = new ApiMeta
                    {
                        TraceId = httpContext.TraceIdentifier,
                        Pagination = pagination
                    }
                });
        }

        var error = result.Error;

        //var problem = new Dictionary<string, object?>
        //{
        //    ["success"] = false,
        //    ["type"] = 
        //    ["title"] = MapTitle(error!.ErrorType),
        //    ["status"] = MapStatusCode(error!.ErrorType),
        //};

        //if (error.ErrorType is ErrorType.Validation)
        //{
        //    problem["errors"] = new Dictionary<string, string[]>
        //    {
        //        [error.Code] = [error.Message]
        //    };
        //}
        //else
        //{
        //    problem["details"] = error.Message;
        //}

        //problem["traceId"] = httpContext.TraceIdentifier;
        //problem["instance"] = httpContext.Request.GetDisplayUrl();

        var problem = new ProblemApiResponse
        {
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            Title = MapTitle(error!.ErrorType),
            Status = MapStatusCode(error!.ErrorType)
        };

        if (error.ErrorType is ErrorType.Validation)
        {
            problem.Errors = new Dictionary<string, string[]>
            {
                [error.Code] = [error.Message]
            };
        }
        else
        {
            problem.Details = error.Message;
        }

        problem.TraceId = httpContext.TraceIdentifier;
        problem.Instance = httpContext.Request.GetDisplayUrl();

        return new ObjectResult(problem)
        {
            StatusCode = MapStatusCode(error!.ErrorType)
        };

        //return new ObjectResult(
        //    new ApiResponse<T>
        //    {
        //        Success = false,
        //        Error = new ApiError(MapStatusCode(error!.ErrorType).ToString(), error?.Message!),
        //        MetaData = new ApiMeta { TraceId = httpContext.TraceIdentifier, Pagination = pagination }
        //    })
        //{ StatusCode = MapStatusCode(error!.ErrorType) };
    }

    public static ActionResult ToApiResponse(this Result result, HttpContext httpContext)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(
                new ApiResponse<object?>
                {
                    Success = true,
                    Data = null,
                    MetaData = new ApiMeta { TraceId = httpContext.TraceIdentifier }
                });
        }

        var error = result.Error;

        var problem = new Dictionary<string, object?>
        {
            ["success"] = false,
            ["type"] = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            ["title"] = MapTitle(error!.ErrorType),
            ["status"] = MapStatusCode(error!.ErrorType),
        };

        if (error.ErrorType is ErrorType.Validation)
        {
            problem["errors"] = new Dictionary<string, string[]>
            {
                [error.Code] = [error.Message]
            };
        }
        else
        {
            problem["details"] = error.Message;
        }

        problem["traceId"] = httpContext.TraceIdentifier;

        return new ObjectResult(problem)
        {
            StatusCode = MapStatusCode(error!.ErrorType)
        };
    }

    private static int MapStatusCode(ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    private static string MapTitle(ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.Validation => "Validation Error",
            ErrorType.NotFound => "Resource Not Found",
            ErrorType.Unauthorized => "Unauthorized Access",
            ErrorType.Forbidden => "Forbidden Access",
            ErrorType.Conflict => "Conflict Error",
            _ => " Internal Server Error"
        };
    }
}
