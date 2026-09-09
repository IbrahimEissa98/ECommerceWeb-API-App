using ECommerceApp.Application.Specifications.Contracts;
using ECommerceApp.Domain.Specifications;
using System.Linq.Expressions;

namespace ECommerceApp.Application.Specifications;

public sealed class OrderSpecificationBuilder<T>(Specification<T> Specification)
    : SpecificationBuilder<T>(Specification), IOrderSpecificationBuilder<T>
    where T : class
{
    public IOrderSpecificationBuilder<T> ThenBy(Expression<Func<T, object?>> expression)
    {
        _specification.AddOrder(new OrderExpressionInfo<T>(expression, OrderType.ThenBy));
        return this;
    }

    public IOrderSpecificationBuilder<T> ThenByDescending(Expression<Func<T, object?>> expression)
    {
        _specification.AddOrder(new OrderExpressionInfo<T>(expression, OrderType.ThenByDescending));
        return this;
    }
}

public sealed class OrderSpecificationBuilder<T, TResult>(Specification<T, TResult> Specification)
    : SpecificationBuilder<T>(Specification), IOrderSpecificationBuilder<T, TResult>
    where T : class
{
    public IOrderSpecificationBuilder<T, TResult> ThenBy(Expression<Func<T, object?>> expression)
    {
        _specification.AddOrder(new OrderExpressionInfo<T>(expression, OrderType.ThenBy));
        return this;
    }

    public IOrderSpecificationBuilder<T, TResult> ThenByDescending(Expression<Func<T, object?>> expression)
    {
        _specification.AddOrder(new OrderExpressionInfo<T>(expression, OrderType.ThenByDescending));
        return this;
    }
}
