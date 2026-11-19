using NooksApi.Data.Enums;

namespace NooksApi.Data.Entities;

public class OpeningPeriod : BaseEntity
{
    public Guid ListingId { get; set; }
    public DayOfWeek Day { get; set; }
    public TimeOnly OpensAt { get; set; }
    public TimeOnly ClosesAt { get; set; }

    public Listing? Listing { get; set; }
}
