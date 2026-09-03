using CloudinaryDotNet;
using ECommerceApp.Application.Common.Services;
using ECommerceApp.Application.ProductBrands;
using ECommerceApp.Application.Products;
using ECommerceApp.Application.ProductTypes;
using ECommerceApp.Domain.Repositories;
using ECommerceApp.Infrastructure.Persistence.Contexts;
using ECommerceApp.Infrastructure.Persistence.Interceptors;
using ECommerceApp.Infrastructure.Persistence.Queries;
using ECommerceApp.Infrastructure.Persistence.Seeding;
using ECommerceApp.Infrastructure.Persistence.Storage;
using ECommerceApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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

        services.AddScoped<IDataSeeder, ProductBrandSeeder>();
        services.AddScoped<IDataSeeder, ProductTypeSeeder>();
        services.AddScoped<IDataSeeder, ProductSeeder>();
        services.AddScoped<DatabaseSeeder>();

        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IProductQueryService, ProductQueryService>();
        services.AddScoped<IProductBrandQueryService, ProductBrandQueryService>();
        services.AddScoped<IProductTypeQueryService, ProductTypeQueryService>();

        services.Configure<CloudinarySettings>(op =>
            config.GetSection("CloudinarySettings").Bind(op)
        );

        //services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

        services.AddSingleton(sp =>
        {
            var settings = sp
                .GetRequiredService<IOptions<CloudinarySettings>>()
                .Value;

            var account = new Account(
                settings.CloudName,
                settings.ApiKey,
                settings.ApiSecret);
            var cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;
            return cloudinary;
        });

        services.AddScoped<IImageService, CloudinaryImageService>();

        return services;
    }
}
