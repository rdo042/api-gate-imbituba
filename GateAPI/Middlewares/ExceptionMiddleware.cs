using GateAPI.Application.Common.Models;
using System.Text.Json;

namespace GateAPI.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception)
            {
                await WriteError(context);
            }
        }

        private static async Task WriteError(HttpContext context)
        {
            //context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var result = Result<object>.Failure("Erro interno no servidor" + context.Response);

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(result));
        }
    }

}
