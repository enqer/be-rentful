using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentful.Domain.Entities;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(a => a.Id);

        builder
            .Property(u => u.Latitude)
            .IsRequired();
        builder
            .Property(u => u.Longitude)
            .IsRequired();

    }
}