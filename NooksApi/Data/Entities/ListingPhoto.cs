namespace NooksApi.Data.Entities;

public class ListingPhoto : BaseEntity
{
    public Guid ListingId { get; set; }
    public string StorageKey { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public Guid? UploadedById { get; set; }

    public Listing? Listing { get; set; }
}
