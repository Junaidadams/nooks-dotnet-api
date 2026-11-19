namespace NooksApi.Data.Entities;

public class ListingOwner
{
    public Guid ListingId { get; set; }
    public Guid UserId { get; set; }
    public DateTime GrantedAt { get; set; } = DateTime.UtcNow;

    public Listing? Listing { get; set; }
}
