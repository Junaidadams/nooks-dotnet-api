using NooksApi.Data.Enums;

namespace NooksApi.Data.Entities;

public class Review : BaseEntity
{
    public Guid ListingId { get; set; }
    public Guid? UserId { get; set; }
    public int Rating { get; set; }
    public string? Body { get; set; }
    public ReviewStatus Status { get; set; } = ReviewStatus.Published;

    public Listing? Listing { get; set; }
}
