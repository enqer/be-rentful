using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Rentful.Application.Common.Interfaces;
using Rentful.Infrastructure.Persistence;

namespace Rentful.Tests.Common.Extensions
{
    internal static class Extensions
    {
        public static IServiceCollection RegisterMemoryDbContext(this IServiceCollection services)
        {
            var dbContextOptions = new DbContextOptionsBuilder<RentfulDbContext>();
            dbContextOptions.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new RentfulDbContext(dbContextOptions.Options);
            dbContext.Database.EnsureDeleted();
            var serviceDescriptor = new ServiceDescriptor(typeof(RentfulDbContext), dbContext);
            services.Replace(serviceDescriptor);
            services.AddSingleton<IRepository>(dbContext);
            return services;
        }
    }
}
