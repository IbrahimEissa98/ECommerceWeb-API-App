using ECommerceApp.Application.Common;
using ECommerceApp.Application.ProductBrands.Queries;
using ECommerceApp.Application.Products.Queries;
using ECommerceApp.Application.ProductTypes.Queries;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceApp.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDIForApplication(this IServiceCollection services)
    {
        var configuration = TypeAdapterConfig.GlobalSettings;
        configuration.Scan(typeof(ProductMappingConfigurations).Assembly);

        services.AddSingleton(configuration);

        services.AddScoped<IMapper, ServiceMapper>();

        services.AddScoped<GetAllProductQuery>();
        services.AddScoped<GetByIdProductQuery>();
        services.AddScoped<GetAllProductBrandsQuery>();
        services.AddScoped<GetAllProductTypesQuery>();

        return services;
    }
}
