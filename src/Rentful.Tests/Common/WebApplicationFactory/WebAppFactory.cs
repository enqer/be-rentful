using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Rentful.Infrastructure.Persistence;
using Rentful.Tests.Common.Extensions;
using Rentful.Tests.Common.Handlers;

namespace Rentful.Tests.Common.WebApplicationFactory
{
    public class WebAppFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.ConfigureDbContext();
                services.AddAuthentication(TestAuthHandler.AuthenticationScheme).AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.AuthenticationScheme, options => { });
            });
            builder.UseEnvironment("Development");
        }
    }

    internal static class AbWebApplicationFactoryExtension
    {
        public static WebApplicationFactory<T> SeedData<T>(this WebApplicationFactory<T> factory, Action<RentfulDbContext> action) where T : class
        {
            return factory.WithWebHostBuilder(builder => builder.ConfigureServices(services =>
            {
                var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<RentfulDbContext>();
                action(dbContext);
                dbContext.SaveChanges();
            }));
        }

        public static WebApplicationFactory<T> Mock<T, K>(this WebApplicationFactory<T> factory, Mock<K> mock) where T : class where K : class
        {
            return factory.WithWebHostBuilder(builder => builder.ConfigureServices(services => services.Mock(mock)));
        }
    }
}
