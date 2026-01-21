using GateAPI.Application.UseCases.Configuracao.UsuarioUC.Criar;
using GateAPI.Application.UseCases.Configuracao.UsuarioUC.Login;
using GateAPI.Requests.Configuracao;
using Microsoft.AspNetCore.Mvc;

namespace GateAPI.Controllers.Configuracao
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        ILogger<AuthController> logger,
        LoginUsuarioHandler loginUsuarioHandler,
        CriarUsuarioHandler criarUsuarioHandler
        ) : BaseController(logger)
    {
        private readonly LoginUsuarioHandler _loginUsuarioHandler = loginUsuarioHandler;
        private readonly CriarUsuarioHandler _criarUsuarioHandler = criarUsuarioHandler;

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioRequest data)
        {
            var query = new LoginUsuarioQuery(data.Email, data.Password);

            var result = await _loginUsuarioHandler.HandleAsync(query);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Usuário não encontrado");
        }

        [HttpPost("/registrar")]
        public async Task<IActionResult> Criar([FromBody] CriarUsuarioCommand data)
        {
            var result = await _criarUsuarioHandler.HandleAsync(data);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao criar usuario");
        }
    }
}
