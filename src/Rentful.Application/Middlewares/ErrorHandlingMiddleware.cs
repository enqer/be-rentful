using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Rentful.Domain.Exceptions;

namespace Rentful.Application.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (HttpResponseException ex)
            {
                context.Response.StatusCode = (int)ex.StatusCode;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    status = ex.StatusCode,
                    title = ex.Value.Title,
                    description = ex.Value.Description
                };

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
            catch (NotFoundException notFound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFound.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
