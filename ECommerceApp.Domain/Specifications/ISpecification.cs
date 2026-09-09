using System.Linq.Expressions;

namespace ECommerceApp.Domain.Specifications;

public interface ISpecification<T> where T : class
{
    IReadOnlyList<Expression<Func<T, bool>>> WhereExpressions { get; }
    IReadOnlyList<Expression<Func<T, object>>> IncludeExpressions { get; }
    IReadOnlyList<IncludeExpressionInfo> ThenIncludeExpressions { get; }
    IReadOnlyList<OrderExpressionInfo<T>> OrderExpressions { get; }
    int? Take { get; }
    int? Skip { get; }
    bool IsPaginationEnabled { get; }
    bool IsTrackingEnabled { get; }
}

public interface ISpecification<T, TResult> where T : class
{
    Expression<Func<T, TResult>>? Selector { get; }
    Expression<Func<T, IEnumerable<TResult>>>? SelectorMany { get; }
}
