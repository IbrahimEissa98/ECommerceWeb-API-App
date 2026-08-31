using ECommerceApp.Application.Common;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ECommerceApp.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDIForApplication(this IServiceCollection services, IConfiguration config)
    {
        var configuration = TypeAdapterConfig.GlobalSettings;
        configuration.Scan(typeof(ProductMappingConfigurations).Assembly);

        services.AddSingleton(configuration);

        services.AddScoped<IMapper, ServiceMapper>();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.LicenseKey = config.GetSection("LuckyPenny:LicenseKey").Value;
        });

        //services.AddScoped<GetAllProductQuery>();
        //services.AddScoped<GetByIdProductQuery>();
        //services.AddScoped<GetAllProductBrandsQuery>();
        //services.AddScoped<GetAllProductTypesQuery>();

        return services;
    }
}
