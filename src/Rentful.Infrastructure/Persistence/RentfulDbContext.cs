using Microsoft.EntityFrameworkCore;
using Rentful.Domain.Entities;
using Rentful.Application.Common.Interfaces;

namespace Rentful.Infrastructure.Persistence
{
    public class RentfulDbContext : DbContext, IRepository
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
