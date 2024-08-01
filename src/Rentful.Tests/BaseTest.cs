using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Rentful.Application;
using Rentful.Application.Common.Interfaces;
using Rentful.Infrastructure;

namespace Rentful.Tests
{
    public abstract class BaseTest
    {
        private IServiceProvider? serviceProvider;
        private IServiceCollection services = RegisterService();

        protected IRepository Repository => ServiceProvider.GetRequiredService<IRepository>();
        protected IMediator Mediator => ServiceProvider.GetRequiredService<IMediator>();

        private IServiceProvider ServiceProvider
        {
            get
            {
                if (serviceProvider == null)
                {
                    serviceProvider = services.BuildServiceProvider();
                }
                return serviceProvider;
            }
            set
            {
                serviceProvider = value;
            }
        }

        protected void Mock<T>(Mock<T> implementation) where T : class
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(T), implementation.Object);
            services.Replace(serviceDescriptor);
        }

        private static IServiceCollection RegisterService()
        {
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x.GetSection("ConnectionStrings")[It.IsAny<string>()]).Returns(string.Empty);
            var services = new ServiceCollection();
            services.AddApplication();
            services.AddInfrastructure(configurationMock.Object);
            services.RegisterMemoryDbContext();
            return services;
        }
    }
}
