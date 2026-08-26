using ECommerceApp.Domain.Common;
using NetArchTest.Rules;
using Xunit;

namespace ECommerceApp.ArchitectureTests;

public class LayerDependencyTests
{
    private const string ApiNamespace = "ECommerceApp.API";
    private const string ApplicationNamespace = "ECommerceApp.Application";
    private const string InfrastructureNamespace = "ECommerceApp.Infrastructure";

    [Fact]
    public void Domain_Should_NotDependOn_OuterLayers()
    {
        var result = Types.InAssembly(typeof(BaseEntity<>).Assembly)
            .ShouldNot()
            .HaveDependencyOnAny(ApiNamespace, ApplicationNamespace, InfrastructureNamespace)
            .GetResult();

        Assert.True(result.IsSuccessful, DescribeFailures(result.FailingTypeNames));
    }

    [Fact]
    public void Application_Should_NotDependOn_ApiOrInfrastructure()
    {
        var result = Types.InAssembly(typeof(ECommerceApp.Application.Extensions.DependencyInjection).Assembly)
            .ShouldNot()
            .HaveDependencyOnAny(ApiNamespace, InfrastructureNamespace)
            .GetResult();

        Assert.True(result.IsSuccessful, DescribeFailures(result.FailingTypeNames));
    }

    [Fact]
    public void Infrastructure_Should_NotDependOn_Api()
    {
        var result = Types.InAssembly(typeof(ECommerceApp.Infrastructure.Extensions.DependencyInjection).Assembly)
            .ShouldNot()
            .HaveDependencyOn(ApiNamespace)
            .GetResult();

        Assert.True(result.IsSuccessful, DescribeFailures(result.FailingTypeNames));
    }

    private static string DescribeFailures(IEnumerable<string> failingTypeNames) =>
        $"Architecture rule violated by: {string.Join(", ", failingTypeNames)}";
}
