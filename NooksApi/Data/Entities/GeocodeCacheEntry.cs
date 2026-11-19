namespace NooksApi.Data.Entities;

public class GeocodeCacheEntry
{
    public string Key { get; set; } = string.Empty;
    public string ResultJson { get; set; } = string.Empty;
    public DateTime CachedAt { get; set; } = DateTime.UtcNow;
}
