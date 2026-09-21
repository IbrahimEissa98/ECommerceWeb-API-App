namespace ECommerceApp.Application.Messaging.Abstractions;

/// <summary>Semantic marker for write/mutation requests.</summary>
public interface ICommand<out TResponse> : IRequest<TResponse>;
