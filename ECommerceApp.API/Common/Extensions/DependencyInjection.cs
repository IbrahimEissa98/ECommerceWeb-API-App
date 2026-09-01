using Asp.Versioning;
using ECommerceApp.API.Middlewares;

namespace ECommerceApp.API.Common.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDIForApi(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandlerMiddleware>();

        //services.AddOpenApi();
        services
            .AddApiVersioning(options =>
            {
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
                //options.DefaultApiVersion = new ApiVersion(2);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            })
            .AddMvc()
            .AddOpenApi();

        return services;
    }
}
