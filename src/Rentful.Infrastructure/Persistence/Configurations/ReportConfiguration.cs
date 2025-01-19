using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentful.Domain.Entities;

namespace Rentful.Infrastructure.Persistence.Configurations
{
    internal class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(a => a.Id);

            builder
                .HasOne(x => x.LeaseAgreement)
                .WithMany(x => x.Reports)
                .HasForeignKey(x => x.LeaseAgreementId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
