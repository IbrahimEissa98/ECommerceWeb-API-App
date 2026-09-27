namespace ECommerceApp.Infrastructure.Caching;

public sealed class RedisSettings
{
    public const string SectionName = "Redis";

    public string ConnectionString { get; init; } = string.Empty;

    public string InstanceName { get; init; } = "ECommerceApp_Basket";
}