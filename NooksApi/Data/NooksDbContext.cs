using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NooksApi.Data.Entities;

namespace NooksApi.Data;

public class NooksDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public NooksDbContext(DbContextOptions<NooksDbContext> options)
        : base(options)
    {
    }

    public DbSet<Listing> Listings => Set<Listing>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Certifier> Certifiers => Set<Certifier>();
    public DbSet<OpeningPeriod> OpeningPeriods => Set<OpeningPeriod>();
    public DbSet<ListingPhoto> ListingPhotos => Set<ListingPhoto>();
    public DbSet<SavedListing> SavedListings => Set<SavedListing>();
    public DbSet<UserCertifierPreference> UserCertifierPreferences => Set<UserCertifierPreference>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<ReviewReport> ReviewReports => Set<ReviewReport>();
    public DbSet<ListingIssueReport> ListingIssueReports => Set<ListingIssueReport>();
    public DbSet<ListingClaim> ListingClaims => Set<ListingClaim>();
    public DbSet<ListingOwner> ListingOwners => Set<ListingOwner>();
    public DbSet<OutboxEmail> OutboxEmails => Set<OutboxEmail>();
    public DbSet<AuditEntry> AuditEntries => Set<AuditEntry>();
    public DbSet<GeocodeCacheEntry> GeocodeCacheEntries => Set<GeocodeCacheEntry>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasPostgresExtension("postgis");

        // Listing
        builder.Entity<Listing>(entity =>
        {
            entity.HasIndex(l => l.Slug).IsUnique();
            entity.HasIndex(l => l.Status);
            entity.HasIndex(l => l.HalaalStatus);
            entity.Property(l => l.Location).HasColumnType("geography(Point,4326)");
            entity.HasOne(l => l.Certifier)
                .WithMany()
                .HasForeignKey(l => l.CertifierId);
            entity.HasOne(l => l.LastVerifiedBy)
                .WithMany()
                .HasForeignKey(l => l.LastVerifiedById);
            entity.HasOne(l => l.SubmittedBy)
                .WithMany()
                .HasForeignKey(l => l.SubmittedById);
        });

        // OpeningPeriod - multiple per day allowed, no unique constraint on (ListingId, Day)
        builder.Entity<OpeningPeriod>(entity =>
        {
            entity.HasIndex(o => o.ListingId);
            entity.HasOne(o => o.Listing)
                .WithMany()
                .HasForeignKey(o => o.ListingId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ListingPhoto
        builder.Entity<ListingPhoto>(entity =>
        {
            entity.HasIndex(p => p.ListingId);
            entity.HasOne(p => p.Listing)
                .WithMany()
                .HasForeignKey(p => p.ListingId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // SavedListing - composite PK, merged saved/favourites
        builder.Entity<SavedListing>(entity =>
        {
            entity.HasKey(s => new { s.UserId, s.ListingId });
            entity.HasOne(s => s.Listing)
                .WithMany()
                .HasForeignKey(s => s.ListingId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // UserCertifierPreference - composite PK
        builder.Entity<UserCertifierPreference>(entity =>
        {
            entity.HasKey(u => new { u.UserId, u.CertifierId });
            entity.HasOne(u => u.Certifier)
                .WithMany()
                .HasForeignKey(u => u.CertifierId);
        });

        // Review - unique (ListingId, UserId)
        builder.Entity<Review>(entity =>
        {
            entity.HasIndex(r => new { r.ListingId, r.UserId }).IsUnique();
            entity.HasOne(r => r.Listing)
                .WithMany()
                .HasForeignKey(r => r.ListingId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ReviewReport - unique (ReviewId, ReporterId)
        builder.Entity<ReviewReport>(entity =>
        {
            entity.HasIndex(r => new { r.ReviewId, r.ReporterId }).IsUnique();
        });

        // ListingIssueReport
        builder.Entity<ListingIssueReport>(entity =>
        {
            entity.HasIndex(r => r.ListingId);
        });

        // ListingClaim
        builder.Entity<ListingClaim>(entity =>
        {
            entity.HasIndex(c => c.ListingId);
            entity.HasIndex(c => c.Status);
            entity.Property(c => c.ProofKeys).HasColumnType("text[]");
        });

        // ListingOwner - composite PK
        builder.Entity<ListingOwner>(entity =>
        {
            entity.HasKey(o => new { o.ListingId, o.UserId });
            entity.HasOne(o => o.Listing)
                .WithMany()
                .HasForeignKey(o => o.ListingId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // OutboxEmail
        builder.Entity<OutboxEmail>(entity =>
        {
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.NextAttemptAt);
        });

        // GeocodeCacheEntry - PK is the normalized query key
        builder.Entity<GeocodeCacheEntry>(entity =>
        {
            entity.HasKey(g => g.Key);
        });

        // Configure Guid v7 generation - handled in entity constructors, not DB-generated
        // Identity tables already use Guid keys
    }

}
