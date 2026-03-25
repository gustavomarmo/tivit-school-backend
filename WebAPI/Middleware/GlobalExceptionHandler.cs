using System.Net;
using System.Text.Json;

namespace edu_connect_backend.WebAPI.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionHandler> logger;
        private readonly IHostEnvironment env;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger, IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ocorreu um erro não tratado: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse();

            switch (exception)
            {
                case KeyNotFoundException:
                    // Erro 404 (Não Encontrado)
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = exception.Message;
                    break;

                case InvalidOperationException:
                case ArgumentException:
                    // Erro 400 (Dados Inválidos)
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = exception.Message;
                    break;

                case UnauthorizedAccessException:
                    // Erro 401 (Não Autorizado)
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.Message = "Acesso negado.";
                    break;

                default:
                    // Erro 500 (Erro Interno - Genérico)
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    response.Message = "Ocorreu um erro interno no servidor. Tente novamente mais tarde.";
                    break;
            }

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, jsonOptions);

            return context.Response.WriteAsync(json);
        }
    }
}
