using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentful.Domain.Entities;

public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
{
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
        builder.HasKey(a => a.Id);

        builder
            .HasOne(a => a.Address)
            .WithMany()
            .HasForeignKey(a => a.AddressId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(a => a.Owner)
            .WithMany(u => u.OwnedApartments)
            .HasForeignKey(a => a.OwnerId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(a => a.Tenant)
            .WithMany(u => u.RentedApartments)
            .HasForeignKey(a => a.TenantId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(a => a.ApartmentDetails)
           .WithOne(ad => ad.Apartment)
           .HasForeignKey<Apartment>(a => a.ApartmentDetailsId)
           .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(a => a.Id)
            .IsRequired();
    }
}
