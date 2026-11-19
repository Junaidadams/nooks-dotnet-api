using NooksApi.Data.Enums;

namespace NooksApi.Data.Entities;

public class ListingIssueReport
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public Guid ListingId { get; set; }
    public Guid? ReporterId { get; set; }
    public ListingIssueReportType Type { get; set; }
    public string? Details { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ResolvedAt { get; set; }
}
