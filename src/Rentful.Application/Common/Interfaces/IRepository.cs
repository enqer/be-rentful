﻿using Microsoft.EntityFrameworkCore;
using Rentful.Domain.Entities;

namespace Rentful.Application.Common.Interfaces
{
    public interface IRepository
    {
        public DbSet<User> Users { get; }
        public DbSet<Address> Addresses { get; }

        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
