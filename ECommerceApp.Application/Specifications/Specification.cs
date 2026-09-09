using ECommerceApp.Application.Specifications.Contracts;
using ECommerceApp.Domain.Specifications;
using System.Linq.Expressions;

namespace ECommerceApp.Application.Specifications;

public abstract class Specification<T> : ISpecification<T> where T : class
{
    private readonly List<Expression<Func<T, bool>>> _whereExpressions = [];
    private readonly List<Expression<Func<T, object>>> _includeExpressions = [];
    private readonly List<IncludeExpressionInfo> _thenIncludeExpressions = [];
    private readonly List<OrderExpressionInfo<T>> _orderExpressions = [];
    //private readonly int? _take;
    //private readonly int? _skip;
    //private readonly bool _isPaginationEnabled;
    //private readonly bool _isTrackingEnabled;

    protected ISpecificationBuilder<T> Query => new SpecificationBuilder<T>(this);

    public IReadOnlyList<Expression<Func<T, bool>>> WhereExpressions => _whereExpressions;
    public IReadOnlyList<Expression<Func<T, object>>> IncludeExpressions => _includeExpressions;
    public IReadOnlyList<IncludeExpressionInfo> ThenIncludeExpressions => _thenIncludeExpressions;
    public IReadOnlyList<OrderExpressionInfo<T>> OrderExpressions => _orderExpressions;

    public int? Take { get; private set; }
    public int? Skip { get; private set; }
    public bool IsPaginationEnabled => Take.HasValue || Skip.HasValue;

    public bool IsTrackingEnabled { get; private set; }

    internal void AddWhere(Expression<Func<T, bool>> expression)
        => _whereExpressions.Add(expression);
    internal Expression<Func<T, object>> AddInclude<TProperty>(Expression<Func<T, TProperty>> navigation)
    {
        var lambda = Expression.Lambda<Func<T, object>>(navigation.Body, navigation.Parameters);
        _includeExpressions.Add(lambda);
        return lambda;
    }
    internal void AddThenInclude(LambdaExpression navigation, LambdaExpression parent)
        => _thenIncludeExpressions.Add(new IncludeExpressionInfo(navigation, parent));
    internal void AddOrder(OrderExpressionInfo<T> orderExpressionInfo)
        => _orderExpressions.Add(orderExpressionInfo);
    internal void SetTake(int take)
        => Take = take;
    internal void SetSkip(int skip)
        => Skip = skip;
    internal void SetTracking()
        => IsTrackingEnabled = true;
    internal void SetNoTracking()
        => IsTrackingEnabled = false;
}

public abstract class Specification<T, TResult> : Specification<T>, ISpecification<T, TResult>
    where T : class
{
    protected new ISpecificationBuilder<T, TResult> Query => new SpecificationBuilder<T, TResult>(this);

    public Expression<Func<T, TResult>>? Selector { get; private set; }
    public Expression<Func<T, IEnumerable<TResult>>>? SelectorMany { get; private set; }

    internal void SetSelector(Expression<Func<T, TResult>> selector)
        => Selector = selector;
    internal void SetSelectorMany(Expression<Func<T, IEnumerable<TResult>>> selector)
        => SelectorMany = selector;
}
