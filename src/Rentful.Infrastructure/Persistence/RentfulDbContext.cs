using Microsoft.EntityFrameworkCore;
using Rentful.Application.Common.Interfaces;
using Rentful.Domain.Entities;
using System.Reflection;

namespace Rentful.Infrastructure.Persistence
{
    public class RentfulDbContext : DbContext, IRepository
    {

        public RentfulDbContext(DbContextOptions<RentfulDbContext> options) : base(options) { }

        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<Image> Images => Set<Image>();
        public DbSet<Location> Locations => Set<Location>();
        public DbSet<Apartment> Apartments => Set<Apartment>();
        public DbSet<Announcement> Announcements => Set<Announcement>();
        public DbSet<LeaseAgreement> LeaseAgreements => Set<LeaseAgreement>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
