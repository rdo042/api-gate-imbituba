using GateAPI.Application.UseCases.Configuracao.UsuarioUC.Atualizar;
using GateAPI.Application.UseCases.Configuracao.UsuarioUC.BuscarPorId;
using GateAPI.Application.UseCases.Configuracao.UsuarioUC.Criar;
using Microsoft.AspNetCore.Mvc;

namespace GateAPI.Controllers.Configuracao
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController(
        ILogger<UsuarioController> logger,
        BuscarPorIdUsuarioHandler buscarPorIdUsuarioHandler,
        CriarUsuarioHandler criarUsuarioHandler,
        AtualizarUsuarioHandler atualizarUsuarioHandler
        ) : BaseController(logger)
    {
        private readonly BuscarPorIdUsuarioHandler _buscarPorIdUsuarioHandler = buscarPorIdUsuarioHandler;
        private readonly CriarUsuarioHandler _criarUsuarioHandler = criarUsuarioHandler;
        private readonly AtualizarUsuarioHandler _atualizarUsuarioHandler = atualizarUsuarioHandler;


        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar([FromRoute] Guid id)
        {
            var query = new BuscarPorIdUsuarioQuery(id);

            var result = await _buscarPorIdUsuarioHandler.HandleAsync(query);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Usuário não encontrado");
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarUsuarioCommand command)
        {
            var result = await _criarUsuarioHandler.HandleAsync(command);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao criar usuario");
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarUsuarioCommand command)
        {
            var result = await _atualizarUsuarioHandler.HandleAsync(command);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao atualizar usuario");
        }
    }
}
