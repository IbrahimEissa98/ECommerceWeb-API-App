using System.Linq.Expressions;

namespace ECommerceApp.Application.Specifications.Contracts;

public interface IOrderSpecificationBuilder<T>
    : ISpecificationBuilder<T> where T : class
{
    IOrderSpecificationBuilder<T> ThenBy(Expression<Func<T, object?>> expression);
    IOrderSpecificationBuilder<T> ThenByDescending(Expression<Func<T, object?>> expression);
}

public interface IOrderSpecificationBuilder<T, TResult>
    : ISpecificationBuilder<T> where T : class
{
    IOrderSpecificationBuilder<T, TResult> ThenBy(Expression<Func<T, object?>> expression);
    IOrderSpecificationBuilder<T, TResult> ThenByDescending(Expression<Func<T, object?>> expression);
}
