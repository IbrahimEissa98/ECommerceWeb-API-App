using ECommerceApp.Application.Specifications.Contracts;
using System.Linq.Expressions;

namespace ECommerceApp.Application.Specifications;

public sealed class IncludeSpecificationBuilder<T, TProperty>(
    Specification<T> Specification,
    LambdaExpression Parent)
    : SpecificationBuilder<T>(Specification), IIncludeSpecificationBuilder<T, TProperty>
    where T : class
{
    private readonly LambdaExpression _parent = Parent;

    public IIncludeSpecificationBuilder<T, TNext> ThenInclude<TNext>(Expression<Func<TProperty, TNext>> navigation)
    {
        _specification.AddThenInclude(navigation, _parent);
        return new IncludeSpecificationBuilder<T, TNext>(_specification, navigation);
    }
}
