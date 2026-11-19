using NooksApi.Data.Enums;

namespace NooksApi.Data.Entities;

public class OutboxEmail
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string To { get; set; } = string.Empty;
    public string Template { get; set; } = string.Empty;
    public string PayloadJson { get; set; } = string.Empty;
    public OutboxEmailStatus Status { get; set; } = OutboxEmailStatus.Pending;
    public int Attempts { get; set; }
    public DateTime? NextAttemptAt { get; set; }
    public string? LastError { get; set; }
    public string? ProviderMessageId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? SentAt { get; set; }
}
