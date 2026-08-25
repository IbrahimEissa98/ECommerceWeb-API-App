namespace ECommerceApp.API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDIForApi(this IServiceCollection services)
    {
        services.AddControllers();

        return services;
    }
}
