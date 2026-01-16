using GateAPI.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GateAPI.Controllers
{
    [ApiController]
    public abstract class BaseController(ILogger logger) : ControllerBase
    {
        protected readonly ILogger _logger = logger;

        protected virtual string Categoria => "Administrativo";

        // SUCCESS
        protected IActionResult OkResponse<T>(T? data, string? message = null)
        {
            return Ok(ApiResponse<T>.Ok(data, message));
        }

        protected IActionResult CreatedResponse<T>(string route, T data)
        {
            LogWithContext("Criado com sucesso.", data);
            return Created(route, ApiResponse<T>.Ok(data, "Criado com sucesso"));
        }

        protected IActionResult AcceptedResponse<T>(string message, T? data)
        {
            LogWithContext(message, data);
            return Accepted(ApiResponse<T>.Ok(data, message));
        }

        protected IActionResult NoContentResponse(string message)
        {
            LogWithContext(message);
            return Ok(ApiResponse<object>.Ok(null, message));
        }

        // ERROR
        protected IActionResult BadRequestResponse(string message, IEnumerable<string>? errors = null)
        {
            LogWithContext(message, errors);
            return BadRequest(ApiResponse<object>.Fail(message, errors));
        }

        protected IActionResult InternalServerError(string? message = null)
        {
            LogWithContext(message ?? "Erro interno no servidor");
            return StatusCode(500, ApiResponse<object>.Fail(message ?? "Erro interno no servidor"));
        }

        private void LogWithContext(string message, object? data = null)
        {
            var userName = HttpContext?.User?.Identity?.Name ?? "System";

            //using (LogContext.PushProperty("CreatedBy", userName))
            //using (LogContext.PushProperty("Categoria", Categoria))
            //{
            //    _logger.LogInformation("{Mensagem} | {@Data}", message, data);
            //}
        }
    }
}
