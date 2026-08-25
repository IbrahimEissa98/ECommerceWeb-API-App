namespace ECommerceApp.Domain.Common;

public interface IHasTimeStamp
{
    DateTimeOffset CreatedAt { get; set; }
    DateTimeOffset? UpdatedAt { get; set; }
    DateTimeOffset? DeletedAt { get; set; }
    bool IsDeleted { get; set; }
}
