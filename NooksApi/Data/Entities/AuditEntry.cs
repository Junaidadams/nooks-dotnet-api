namespace NooksApi.Data.Entities;

public class AuditEntry
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public Guid? ActorId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public Guid? EntityId { get; set; }
    public string? ChangesJson { get; set; }
    public DateTime At { get; set; } = DateTime.UtcNow;
}
