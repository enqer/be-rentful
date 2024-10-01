using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentful.Domain.Entities;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(a => a.Id);

        builder
            .Property(a => a.PostalCode)
            .IsRequired()
            .HasMaxLength(6);
        builder
            .Property(a => a.BuildingNumber)
            .IsRequired()
            .HasMaxLength(10);
        builder
            .Property(a => a.Street)
            .IsRequired()
            .HasMaxLength(100);
        builder
            .Property(a => a.City)
            .IsRequired()
            .HasMaxLength(50);
        builder
            .Property(a => a.Country)
            .IsRequired()
            .HasMaxLength(50);
    }
}
