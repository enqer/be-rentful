using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rentful.Application.Common.Interfaces;
using Rentful.Infrastructure.Persistence;
using Rentful.Infrastructure.Services;

namespace Rentful.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Rentful");
            services.AddDbContext<IRepository, RentfulDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<IUserResolver, UserResolver>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddTransient<IMessagePublisher, MessagePublisher>();
            return services;
        }
        public static WebApplication ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<RentfulDbContext>();
            if (db != null)
            {
                if (db.Database.GetPendingMigrations().Any())
                {
                    db.Database.Migrate();
                }
            }
            return app;
        }
    }
}
