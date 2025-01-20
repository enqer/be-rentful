using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentful.Domain.Entities;

namespace Rentful.Infrastructure.Persistence.Configurations
{
    internal class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(a => a.Id);

            builder
                .HasOne(x => x.LeaseAgreement)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.LeaseAgreementId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
