using ECommerceApp.API.Middlewares;

namespace ECommerceApp.API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDIForApi(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandlerMiddleware>();

        return services;
    }
}
