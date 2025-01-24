using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using Rentful.Domain.Options;
using System.Text;

namespace Rentful.Api
{
    public static class DependencyInjection
    {
        public static void ConfigureOptions(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.ConfigureOption<JwtSettings>();
            webApplicationBuilder.ConfigureOption<MailSettings>();
        }

        private static WebApplicationBuilder ConfigureOption<T>(this WebApplicationBuilder webApplicationBuilder) where T : class
        {
            webApplicationBuilder.Services.Configure<T>(webApplicationBuilder.Configuration.GetSection(typeof(T).Name));
            return webApplicationBuilder;
        }

        public static void ConfigureAuthentication(this WebApplicationBuilder webApplicationBuilder, IConfiguration configuration)
        {
            webApplicationBuilder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
        }

        public static void ConfigureLogging(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Information);
            });
            webApplicationBuilder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();
        }

        public static void ConfigureMassTransit(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Host.ConfigureServices((hostContext, services) =>
            {
                services.AddMassTransit(x =>
                {
                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host("localhost", "/", h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });

                        cfg.ConfigureEndpoints(context);
                    });
                });
            });
        }
    }
}
