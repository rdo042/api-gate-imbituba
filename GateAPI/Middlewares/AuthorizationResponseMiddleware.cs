using GateAPI.Application.Common.Models;
using System.Text.Json;

namespace GateAPI.Middlewares
{
    public class AuthorizationResponseMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.HasStarted)
                return;

            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                await WriteResponse(
                    context,
                    StatusCodes.Status401Unauthorized,
                    "Usuário não autenticado");
            }
            else if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                await WriteResponse(
                    context,
                    StatusCodes.Status403Forbidden,
                    "Permissão insuficiente");
            }
        }

        private static async Task WriteResponse(
            HttpContext context,
            int statusCode,
            string message)
        {
            context.Response.Clear();
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var result = Result<object>.Failure(message);

            var json = JsonSerializer.Serialize(
                result,
                _jsonOptions);

            await context.Response.WriteAsync(json);
        }
    }
}
