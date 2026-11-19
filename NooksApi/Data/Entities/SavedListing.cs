namespace NooksApi.Data.Entities;

public class SavedListing
{
    public Guid UserId { get; set; }
    public Guid ListingId { get; set; }
    public DateTime SavedAt { get; set; } = DateTime.UtcNow;

    public Listing? Listing { get; set; }
}
