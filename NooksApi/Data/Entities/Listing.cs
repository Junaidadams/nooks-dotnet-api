using NetTopologySuite.Geometries;
using NooksApi.Data.Enums;

namespace NooksApi.Data.Entities;

public class Listing : BaseEntity
{
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? AddressLine { get; set; }
    public string? Suburb { get; set; }
    public Point? Location { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public ListingStatus Status { get; set; } = ListingStatus.Pending;
    public HalaalStatus HalaalStatus { get; set; } = HalaalStatus.NotCertified;
    public int? CertifierId { get; set; }
    public DateTime? CertificateExpiresOn { get; set; }
    public string? HalaalNote { get; set; }
    public DateTime? LastVerifiedAt { get; set; }
    public Guid? LastVerifiedById { get; set; }
    public Guid? SubmittedById { get; set; }
    public double RatingAverage { get; set; }
    public int RatingCount { get; set; }

    public Certifier? Certifier { get; set; }
    public AppUser? LastVerifiedBy { get; set; }
    public AppUser? SubmittedBy { get; set; }
}
