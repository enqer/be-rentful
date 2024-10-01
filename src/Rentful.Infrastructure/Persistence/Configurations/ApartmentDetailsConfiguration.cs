using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentful.Domain.Entities;

public class ApartmentDetailsConfiguration : IEntityTypeConfiguration<ApartmentDetails>
{
    public void Configure(EntityTypeBuilder<ApartmentDetails> builder)
    {
        builder.HasKey(ad => ad.Id);

        builder
            .HasOne(ad => ad.Apartment)
            .WithOne(a => a.ApartmentDetails)
            .HasForeignKey<Apartment>(a => a.ApartmentDetailsId);
    }
}
