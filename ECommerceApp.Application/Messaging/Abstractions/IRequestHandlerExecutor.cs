namespace ECommerceApp.Application.Messaging.Abstractions;

internal interface IRequestHandlerExecutor
{
    Type RequestType { get; }

    Task<object?> ExecuteAsync(object request, CancellationToken cancellationToken);
}
