namespace ECommerceApp.Domain.Common;

public class BaseEntity<PKType> : IHasTimeStamp where PKType : struct
{
    public PKType Id { get; protected set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }

    protected void SetId(PKType id)
    {
        Id = id;
    }

    //public string CreatedBy { get; protected set; } = default!;
    //public string UpdatedBy { get; protected set; } = default!;
}
