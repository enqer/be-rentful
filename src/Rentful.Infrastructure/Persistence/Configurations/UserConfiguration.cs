using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentful.Domain.Entities;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder
            .HasOne(u => u.Address)
            .WithOne()
            .HasForeignKey<User>(u => u.AddressId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.Announcements)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);
        builder
            .Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        builder
            .Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(50);
        builder
            .Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(200);
    }
}
