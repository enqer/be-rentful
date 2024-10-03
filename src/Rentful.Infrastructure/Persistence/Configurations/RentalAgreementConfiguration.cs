using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentful.Domain.Entities;

public class RentalAgreementConfiguration : IEntityTypeConfiguration<RentalAgreement>
{
    public void Configure(EntityTypeBuilder<RentalAgreement> builder)
    {
        builder.HasKey(ra => ra.Id);

        builder
            .HasOne(ra => ra.Tenant)
            .WithMany(u => u.RentedApartments)
            .HasForeignKey(ra => ra.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ra => ra.Apartment)
            .WithMany(a => a.RentalAgreements)
            .HasForeignKey(ra => ra.ApartmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
