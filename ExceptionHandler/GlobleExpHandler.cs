using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ContactManagementSystem.ExceptionHandler
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GlobleExpHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobleExpHandler> _logger;

        public GlobleExpHandler(RequestDelegate next, ILogger<GlobleExpHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            //return _next(httpContext);
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(httpContext, ex).ConfigureAwait(false);
            }
        }
        private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                ErrorMessage = exception.Message
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class GlobleExpHandlerExtensions
    {
        public static void UseGlobleExpHandler(this IApplicationBuilder builder)
        {
             builder.UseMiddleware<GlobleExpHandler>();
        }
    }
}
