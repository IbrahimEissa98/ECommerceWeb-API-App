using System.Linq.Expressions;

namespace ECommerceApp.Application.Specifications.Contracts;

public interface IIncludeCollectionSpecificationBuilder<T, TElement>
    : ISpecificationBuilder<T> where T : class
{
    IIncludeSpecificationBuilder<T, TNext> ThenInclude<TNext>(
        Expression<Func<TElement, TNext>> navigation);
}
