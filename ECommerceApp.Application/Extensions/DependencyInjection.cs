using Microsoft.Extensions.DependencyInjection;

namespace ECommerceApp.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDIForApplication(this IServiceCollection services)
    {
        return services;
    }
}
