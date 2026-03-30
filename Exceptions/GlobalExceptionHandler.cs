using System.Net;
using System.Text.Json;

namespace edu_connect_backend.Exceptions
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionHandler> logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            (int statusCode, string message) = exception switch
            {
                KeyNotFoundException => (404, exception.Message),
                InvalidOperationException => (400, exception.Message),
                ArgumentException => (400, exception.Message),
                UnauthorizedAccessException => (401, "Acesso negado."),
                _ => (500, "Ocorreu um erro interno no servidor. Tente novamente mais tarde.")
            };

            context.Response.StatusCode = statusCode;

            var json = JsonSerializer.Serialize(
                new { message },
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return context.Response.WriteAsync(json);
        }
    }
}