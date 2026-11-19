using Microsoft.AspNetCore.Identity;
using NooksApi.Data.Enums;

namespace NooksApi.Data.Entities;

public class AppUser : IdentityUser<Guid>
{
    public string? DisplayName { get; set; }
    public string? AvatarKey { get; set; }
    public UserTier Tier { get; set; } = UserTier.Free;
    public bool ShowNotCertified { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
