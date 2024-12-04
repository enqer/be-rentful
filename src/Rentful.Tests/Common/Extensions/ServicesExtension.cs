using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Rentful.Application.Common.Interfaces;
using Rentful.Infrastructure.Persistence;

namespace Rentful.Tests.Common.Extensions
{
    internal static class ServicesExtension
    {
        public static void Mock<T>(this IServiceCollection services, Mock<T> mock) where T : class
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(T), mock.Object);
            services.Replace(serviceDescriptor);
        }

        public static void ConfigureDbContext(this IServiceCollection services)
        {
            var dbContextOptions = new DbContextOptionsBuilder<RentfulDbContext>();
            dbContextOptions.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new RentfulDbContext(dbContextOptions.Options);
            dbContext.Database.EnsureCreated();
            var dbContextDescriptor = new ServiceDescriptor(typeof(RentfulDbContext), dbContext);
            var repositoryDescriptor = new ServiceDescriptor(typeof(IRepository), dbContext);
            services.Replace(dbContextDescriptor);
            services.Replace(repositoryDescriptor);
        }
    }
}
