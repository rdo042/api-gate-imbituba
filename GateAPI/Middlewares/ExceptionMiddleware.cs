using FluentValidation;
using GateAPI.Application.Common.Models;
using GateAPI.Responses;
using System.Text.Json;

namespace GateAPI.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            //TODO: Capturar exceções da external !!?? A Criar
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationException(context, ex);
            }
            catch (Exception)
            {
                await WriteError(context);
            }
        }

        private static async Task HandleValidationException(
       HttpContext context,
       ValidationException exception)
        {
            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var errors = exception.Errors
            .Select(e => e.ErrorMessage)
            .Distinct()
            .ToList();

            var result = ApiResponse<object>.Fail("Erro de validação", errors);

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(result));
        }
        private static async Task WriteError(HttpContext context)
        {
            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var result = ApiResponse<object>.Fail("Erro interno no servidor");

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(result));
        }
    }
}