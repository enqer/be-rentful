using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Rentful.Application.Middlewares
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status400BadRequest &&
                context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                var actionContext = context.RequestServices.GetService<IActionContextAccessor>()?.ActionContext;

                if (actionContext != null)
                {
                    var modelState = actionContext.ModelState;

                    if (!modelState.IsValid)
                    {
                        context.Response.Clear();
                        context.Response.ContentType = "application/json";
                        var errors = modelState.Values.SelectMany(v => v.Errors)
                                                       .Select(e => e.ErrorMessage)
                                                       .ToArray();

                        await context.Response.WriteAsJsonAsync(new
                        {
                            Errors = errors
                        });
                    }
                }
            }
        }
    }
}
