using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentful.Domain.Entities;

namespace Rentful.Infrastructure.Persistence.Configurations
{
    public class LeaseAgreementConfiguration : IEntityTypeConfiguration<LeaseAgreement>
    {
        public void Configure(EntityTypeBuilder<LeaseAgreement> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .HasOne(x => x.Apartment)
                .WithMany(x => x.LeaseAgreements)
                .HasForeignKey(x => x.ApartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Tenant)
                .WithMany(x => x.LeaseAgreements)
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
