using System.Linq.Expressions;

namespace ECommerceApp.Domain.Specifications;

public sealed record OrderExpressionInfo<T>(
    Expression<Func<T, object?>> OrderKey,
    OrderType OrderType);

public enum OrderType
{
    OrderBy,
    OrderByDescending,
    ThenBy,
    ThenByDescending
}