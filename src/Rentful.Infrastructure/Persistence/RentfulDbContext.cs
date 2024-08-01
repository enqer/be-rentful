using Microsoft.EntityFrameworkCore;
using Rentful.Domain.Entities;

namespace Rentful.Infrastructure.Persistence
{
    internal class RentfulDbContext : DbContext
    {

        public RentfulDbContext(DbContextOptions<RentfulDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RentfulDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
