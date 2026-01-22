using GateAPI.Application.UseCases.Configuracao.PerfilUC.Atualizar;
using GateAPI.Application.UseCases.Configuracao.PerfilUC.BuscarPorId;
using GateAPI.Application.UseCases.Configuracao.PerfilUC.BuscarTodos;
using GateAPI.Application.UseCases.Configuracao.PerfilUC.Criar;
using GateAPI.Application.UseCases.Configuracao.PerfilUC.Deletar;
using Microsoft.AspNetCore.Mvc;

namespace GateAPI.Controllers.Configuracao
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController(
        ILogger<PerfilController> logger,
        BuscarPorIdPerfilHandler buscarPorIdPerfilHandler,
        BuscarTodosPerfilHandler buscarTodosPerfilHandler,
        CriarPerfilHandler criarPerfilHandler,
        AtualizarPerfilHandler atualizarPerfilHandler,
        DeletarPerfilHandler deletarPerfilHandler
        ) : BaseController(logger)
    {
        private readonly BuscarPorIdPerfilHandler _buscarPorIdPerfilHandler = buscarPorIdPerfilHandler;
        private readonly BuscarTodosPerfilHandler _buscarTodosPerfilHandler = buscarTodosPerfilHandler;
        private readonly CriarPerfilHandler _criarPerfilHandler = criarPerfilHandler;
        private readonly AtualizarPerfilHandler _atualizarPerfilHandler = atualizarPerfilHandler;
        private readonly DeletarPerfilHandler _deletarPerfilHandler = deletarPerfilHandler;

        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar([FromRoute] Guid id)
        {
            var query = new BuscarPorIdPerfilQuery(id);

            var result = await _buscarPorIdPerfilHandler.HandleAsync(query);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao buscar Perfil");
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var query = new BuscarTodosPerfilQuery();

            var result = await _buscarTodosPerfilHandler.HandleAsync(query);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao buscar lista");
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarPerfilCommand command)
        {
            var result = await _criarPerfilHandler.HandleAsync(command);

            return result.IsSuccess ? CreatedResponse(nameof(Criar), result.Data) : BadRequestResponse(result.Error ?? "Erro ao criar Perfil");
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarPerfilCommand command)
        {
            var result = await _atualizarPerfilHandler.HandleAsync(command);

            return result.IsSuccess ? NoContentResponse("Sucesso ao atualizar Perfil") : BadRequestResponse(result.Error ?? "Erro ao atualizar Perfil");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover([FromRoute] Guid id)
        {
            var query = new DeletarPerfilCommand(id);

            var result = await _deletarPerfilHandler.HandleAsync(query);

            return result.IsSuccess ? NoContentResponse("Sucesso ao remover Perfil") : BadRequestResponse(result.Error ?? "Erro ao remover Perfil");
        }
    }
}
