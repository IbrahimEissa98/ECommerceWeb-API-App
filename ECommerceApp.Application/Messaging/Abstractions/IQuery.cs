namespace ECommerceApp.Application.Messaging.Abstractions;

/// <summary>Semantic marker for read-only requests.</summary>
public interface IQuery<out TResponse> : IRequest<TResponse>;
