using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Rentful.Application.Middlewares;
using Rentful.Application.Validators;
using System.Reflection;

namespace Rentful.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<ErrorHandlingMiddleware>();
            return services;
        }
    }
}
