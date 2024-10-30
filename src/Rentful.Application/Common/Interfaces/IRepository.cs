using Microsoft.EntityFrameworkCore;
using Rentful.Domain.Entities;

namespace Rentful.Application.Common.Interfaces
{
    public interface IRepository
    {
        public DbSet<User> Users { get; }
        public DbSet<Address> Addresses { get; }
        public DbSet<Image> Images { get; }
        public DbSet<Location> Locations { get; }
        public DbSet<Apartment> Apartments { get; }
        public DbSet<Announcement> Announcements { get; }

        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
