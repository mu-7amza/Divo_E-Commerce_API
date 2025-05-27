using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Divo.Errors;

namespace Divo.Middleware
{
    public class ExceptionMiddlware(RequestDelegate next, ILogger<ExceptionMiddlware> logger, IHostEnvironment env)
    {

        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddlware> _logger = logger;
        private readonly IHostEnvironment _env = env;

        public async Task InvokeAsync(HttpContext context){

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "applications/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                _logger.LogError(ex, ex.Message);

                var response = _env.IsDevelopment()
                ? new APiException((int)HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace.ToString())
                : new APiException((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
        
    }
}