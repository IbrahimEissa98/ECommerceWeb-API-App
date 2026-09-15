using ECommerceApp.Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerceApp.Infrastructure.Persistence.Specifications;

public static class SpecificationEvaluator
{
    public static IQueryable<T> GetQuery<T>(
        IQueryable<T> inputQuery,
        ISpecification<T> specification,
        bool applyPagination = true)
        where T : class
    {
        var query = inputQuery;

        query = ApplyPredicates(query, specification);
        query = ApplyIncludes(query, specification);
        query = ApplyOrdering(query, specification);

        if (applyPagination)
            query = ApplyPagination(query, specification);

        query = ApplyTracking(query, specification);

        return query;
    }

    public static IQueryable<T> GetCountQuery<T>(IQueryable<T> inputQuery, ISpecification<T> specification)
        where T : class
    {
        var query = ApplyPredicates(inputQuery, specification);
        return ApplyTracking(query, specification);
    }

    public static IQueryable<TResult> GetQuery<T, TResult>(
        IQueryable<T> inputQuery,
        ISpecification<T, TResult> specification)
        where T : class
    {
        var query = GetQuery(inputQuery, (ISpecification<T>)specification);

        if (specification.Selector is not null)
            return query.Select(specification.Selector);

        if (specification.SelectorMany is not null)
            return query.SelectMany(specification.SelectorMany);

        throw new InvalidOperationException(
            $"Specification {specification.GetType().Name} must define either Select or SelectMany.");
    }

    private static IQueryable<T> ApplyPredicates<T>(
        IQueryable<T> query,
        ISpecification<T> specification)
        where T : class
    {
        foreach (var whereExpression in specification.WhereExpressions)
            query = query.Where(whereExpression);

        return query;
    }

    // apply includes and then includes

    // include(o => o.Customer)
    // thenInclude(c => c.Address)                           // include("Customer.Address")
    // .include(o => o.items)
    //  .ThenInclude(item => item.Product)
    private static IQueryable<T> ApplyIncludes<T>(
        IQueryable<T> query,
        ISpecification<T> specification)
        where T : class
    {
        if (specification.IncludeExpressions.Count == 0)
            return query;

        var pathMap = new Dictionary<LambdaExpression, string>();

        foreach (var include in specification.IncludeExpressions)
        {
            var path = GetMemberPath(include.Body); // customer
            query = query.Include(path); // query = query.include("customer")
            pathMap[include] = path;  // key: o => o.Customer , value: customer
        }

        foreach (var includeInfo in specification.ThenIncludeExpressions)
        {
            if (!pathMap.TryGetValue(includeInfo.PreviousInclude, out var parentPath))
            {
                throw new InvalidOperationException(
                    $"Could not find parent expression for ThenInclude: {includeInfo.LambdaExpression}");
            }

            var path = $"{parentPath}.{GetMemberPath(includeInfo.LambdaExpression.Body)}";
            query = query.Include(path); // Customer.Address
            pathMap[includeInfo.LambdaExpression] = path;
        }

        return query;
    }

    // o => o.Customer (body is MemberExpression)  , Member(Customer) .Name
    private static string GetMemberPath(Expression expression)
    {
        if (expression is MemberExpression member)
            return member.Member.Name;

        throw new NotSupportedException(
            $"Include expression '{expression}' must be a simple member access (e.g. x => x.Property).");
    }

    private static IQueryable<T> ApplyOrdering<T>(
        IQueryable<T> query,
        ISpecification<T> specification)
        where T : class
    {
        if (specification.OrderExpressions.Count == 0)
            return query;

        IOrderedQueryable<T>? orderedQuery = null;

        foreach (var orderExpression in specification.OrderExpressions)
        {
            switch (orderExpression.OrderType)
            {
                case OrderType.OrderBy:
                    orderedQuery = query.OrderBy(orderExpression.OrderKey);
                    query = orderedQuery;
                    break;

                case OrderType.OrderByDescending:
                    orderedQuery = query.OrderByDescending(orderExpression.OrderKey);
                    query = orderedQuery;
                    break;

                case OrderType.ThenBy:
                    orderedQuery = orderedQuery!.ThenBy(orderExpression.OrderKey);
                    query = orderedQuery;
                    break;

                case OrderType.ThenByDescending:
                    orderedQuery = orderedQuery!.ThenByDescending(orderExpression.OrderKey);
                    query = orderedQuery;
                    break;

                default:
                    throw new InvalidOperationException(
                        $"Unsupported order type: {orderExpression.OrderType}");
            }
        }

        return query;
    }

    private static IQueryable<T> ApplyPagination<T>(
        IQueryable<T> query,
        ISpecification<T> specification)
        where T : class
    {
        if (!specification.IsPaginationEnabled)
            return query;

        if (specification.Skip is int skip)
            query = query.Skip(skip);

        if (specification.Take is int take)
            query = query.Take(take);

        return query;
    }

    private static IQueryable<T> ApplyTracking<T>(
        IQueryable<T> query,
        ISpecification<T> specification)
        where T : class
    {
        if (!specification.IsTrackingEnabled)
            query = query.AsNoTracking();

        return query;
    }
}
