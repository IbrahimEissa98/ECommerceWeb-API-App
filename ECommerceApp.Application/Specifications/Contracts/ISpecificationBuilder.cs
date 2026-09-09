using System.Linq.Expressions;

namespace ECommerceApp.Application.Specifications.Contracts;

public interface ISpecificationBuilder<T>
    where T : class
{
    ISpecificationBuilder<T> Where(Expression<Func<T, bool>> expression);

    IIncludeSpecificationBuilder<T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> navigation);
    IIncludeCollectionSpecificationBuilder<T, TElement> Include<TElement>(
        Expression<Func<T, ICollection<TElement>>> navigation);

    IOrderSpecificationBuilder<T> OrderBy(Expression<Func<T, object?>> expression);
    IOrderSpecificationBuilder<T> OrderByDescending(Expression<Func<T, object?>> expression);

    ISpecificationBuilder<T> Take(int take);
    ISpecificationBuilder<T> Skip(int skip);
    ISpecificationBuilder<T> AsNoTracking();
    ISpecificationBuilder<T> AsTracking();
}

public interface ISpecificationBuilder<T, TResult>
    where T : class
{
    ISpecificationBuilder<T, TResult> Where(Expression<Func<T, bool>> expression);

    //IIncludeSpecificationBuilder<T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> navigation);
    //IIncludeCollectionSpecificationBuilder<T, TElement> Include<TElement>(
    //    Expression<Func<T, ICollection<TElement>>> navigation);

    IOrderSpecificationBuilder<T, TResult> OrderBy(Expression<Func<T, object?>> expression);
    IOrderSpecificationBuilder<T, TResult> OrderByDescending(Expression<Func<T, object?>> expression);

    ISpecificationBuilder<T, TResult> Take(int take);
    ISpecificationBuilder<T, TResult> Skip(int skip);
    ISpecificationBuilder<T, TResult> AsNoTracking();
    ISpecificationBuilder<T, TResult> AsTracking();

    ISpecificationBuilder<T, TResult> Select(Expression<Func<T, TResult>> selector);
    ISpecificationBuilder<T, TResult> SelectMany(Expression<Func<T, IEnumerable<TResult>>> selector);
}
