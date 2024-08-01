using Rentful.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Rentful.Application.Common.Interfaces
{
    public interface IRepository
    {
        public DbSet<User> Users { get; }

        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
