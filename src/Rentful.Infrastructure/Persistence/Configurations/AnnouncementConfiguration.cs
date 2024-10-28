using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentful.Domain.Entities;

public class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
{
    public void Configure(EntityTypeBuilder<Announcement> builder)
    {
        builder
            .HasKey(a => a.Id);
        builder
           .HasOne(u => u.Apartment)
           .WithOne()
           .HasForeignKey<Announcement>(x => x.ApartmentId)
           .OnDelete(DeleteBehavior.Cascade);
        builder
            .Property(u => u.Title)
            .IsRequired();
        builder
            .Property(u => u.Description)
            .IsRequired();
        builder
            .Property(u => u.DateAdded)
            .IsRequired();

    }
}
