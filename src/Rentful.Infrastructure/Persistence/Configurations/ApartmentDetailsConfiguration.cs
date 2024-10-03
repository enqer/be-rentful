using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentful.Domain.Entities;

public class ApartmentDetailsConfiguration : IEntityTypeConfiguration<ApartmentDetails>
{
    public void Configure(EntityTypeBuilder<ApartmentDetails> builder)
    {
        builder.HasKey(ad => ad.Id);

        builder
            .HasOne(a => a.Owner)
            .WithMany(u => u.OwnedApartments)
            .HasForeignKey(a => a.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(a => a.RentalAgreements)
            .WithOne(ra => ra.Apartment)
            .HasForeignKey(ra => ra.ApartmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
