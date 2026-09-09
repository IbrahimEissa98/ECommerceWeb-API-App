using System.Linq.Expressions;

namespace ECommerceApp.Domain.Specifications;

public sealed record IncludeExpressionInfo(
    LambdaExpression LambdaExpression,
    LambdaExpression PreviousInclude);