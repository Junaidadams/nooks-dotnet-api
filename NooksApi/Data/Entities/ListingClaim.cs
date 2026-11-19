using NooksApi.Data.Enums;

namespace NooksApi.Data.Entities;

public class ListingClaim
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public Guid ListingId { get; set; }
    public Guid UserId { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? ContactPhone { get; set; }
    public List<string> ProofKeys { get; set; } = new();
    public ListingClaimStatus Status { get; set; } = ListingClaimStatus.Pending;
    public Guid? ReviewedById { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public string? ReviewNote { get; set; }

    public Listing? Listing { get; set; }
}
