using ECommerceApp.Application.Specifications.Contracts;
using ECommerceApp.Domain.Specifications;
using System.Linq.Expressions;

namespace ECommerceApp.Application.Specifications;

public class SpecificationBuilder<T>(Specification<T> Specification) : ISpecificationBuilder<T>
    where T : class
{
    protected readonly Specification<T> _specification = Specification;

    public ISpecificationBuilder<T> Where(Expression<Func<T, bool>> expression)
    {
        _specification.AddWhere(expression);
        return this;
    }

    public IIncludeSpecificationBuilder<T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> navigation)
    {
        var parent = _specification.AddInclude(navigation);
        return new IncludeSpecificationBuilder<T, TProperty>(_specification, parent);
    }

    public IIncludeCollectionSpecificationBuilder<T, TElement> Include<TElement>(Expression<Func<T, ICollection<TElement>>> navigation)
    {
        var parent = _specification.AddInclude(navigation);
        return new IncludeCollectionSpecificationBuilder<T, TElement>(_specification, parent);
    }

    public IOrderSpecificationBuilder<T> OrderBy(Expression<Func<T, object?>> expression)
    {
        _specification.AddOrder(new OrderExpressionInfo<T>(expression, OrderType.OrderBy));
        return new OrderSpecificationBuilder<T>(_specification);
    }

    public IOrderSpecificationBuilder<T> OrderByDescending(Expression<Func<T, object?>> expression)
    {
        _specification.AddOrder(new OrderExpressionInfo<T>(expression, OrderType.OrderByDescending));
        return new OrderSpecificationBuilder<T>(_specification);
    }

    public ISpecificationBuilder<T> Take(int take)
    {
        _specification.SetTake(take);
        return this;
    }

    public ISpecificationBuilder<T> Skip(int skip)
    {
        _specification.SetSkip(skip);
        return this;
    }

    public ISpecificationBuilder<T> AsNoTracking()
    {
        _specification.SetNoTracking();
        return this;
    }

    public ISpecificationBuilder<T> AsTracking()
    {
        _specification.SetTracking();
        return this;
    }
}

public class SpecificationBuilder<T, TResult>(Specification<T, TResult> Specification) : ISpecificationBuilder<T, TResult>
    where T : class
{
    protected readonly Specification<T, TResult> _specification = Specification;
    protected readonly SpecificationBuilder<T> _builder = new(Specification);

    ISpecificationBuilder<T, TResult> ISpecificationBuilder<T, TResult>.Where(Expression<Func<T, bool>> expression)
    {
        _builder.Where(expression);
        return this;
    }

    //public IIncludeSpecificationBuilder<T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> navigation)
    //{
    //    var parent = _specification.AddInclude(navigation);
    //    return IncludeSpecificationBuilder<T, TProperty>(_specification, parent);
    //}

    //public IIncludeCollectionSpecificationBuilder<T, TElement> Include<TElement>(Expression<Func<T, ICollection<TElement>>> navigation)
    //{
    //    var parent = _specification.AddInclude(navigation);
    //    return IncludeCollectionSpecificationBuilder<T, TElement>(_specification, parent);
    //}

    public IOrderSpecificationBuilder<T, TResult> OrderBy(Expression<Func<T, object?>> expression)
    {
        _builder.OrderBy(expression);
        return new OrderSpecificationBuilder<T, TResult>(_specification);
    }

    public IOrderSpecificationBuilder<T, TResult> OrderByDescending(Expression<Func<T, object?>> expression)
    {
        _builder.OrderByDescending(expression);
        return new OrderSpecificationBuilder<T, TResult>(_specification);
    }

    public ISpecificationBuilder<T, TResult> Take(int take)
    {
        _builder.Take(take);
        return this;
    }

    public ISpecificationBuilder<T, TResult> Skip(int skip)
    {
        _builder.Skip(skip);
        return this;
    }

    public ISpecificationBuilder<T, TResult> AsNoTracking()
    {
        _builder.AsNoTracking();
        return this;
    }

    public ISpecificationBuilder<T, TResult> AsTracking()
    {
        _builder.AsTracking();
        return this;
    }

    public ISpecificationBuilder<T, TResult> Select(Expression<Func<T, TResult>> selector)
    {
        _specification.SetSelector(selector);
        return this;
    }

    public ISpecificationBuilder<T, TResult> SelectMany(Expression<Func<T, IEnumerable<TResult>>> selector)
    {
        _specification.SetSelectorMany(selector);
        return this;
    }
}
