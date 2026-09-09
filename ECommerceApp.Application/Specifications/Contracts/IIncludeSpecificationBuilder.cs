using System.Linq.Expressions;

namespace ECommerceApp.Application.Specifications.Contracts;

public interface IIncludeSpecificationBuilder<T, TProperty>
    : ISpecificationBuilder<T> where T : class
{
    IIncludeSpecificationBuilder<T, TNext> ThenInclude<TNext>(Expression<Func<TProperty, TNext>> navigation);
}
