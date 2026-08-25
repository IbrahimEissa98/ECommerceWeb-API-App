using ECommerceApp.Infrastructure.Persistence.Contexts;
using ECommerceApp.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ECommerceApp.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDIForInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("The Default Connection string not found");

        services.AddSingleton<TimestampInterceptor>();
        services.AddDbContext<ECommerceDbContext>((sp, options) =>
        {
            options.UseSqlServer(connectionString);
            options.LogTo(Console.WriteLine, LogLevel.Information);
            options.AddInterceptors(sp.GetRequiredService<TimestampInterceptor>());
        });

        return services;
    }
}
