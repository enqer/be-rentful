using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentful.Domain.Entities;

public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
{
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
        builder.HasKey(a => a.Id);

        builder
           .HasOne(u => u.Location)
           .WithOne()
           .HasForeignKey<Apartment>(x => x.LocationId)
           .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.Images)
            .WithOne(x => x.Apartment)
            .HasForeignKey(x => x.ApartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(u => u.Area)
            .IsRequired();

        builder
            .Property(u => u.Price)
            .IsRequired();

    }
}
