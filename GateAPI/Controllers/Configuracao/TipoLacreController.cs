using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Atualizar;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.BuscarPorId;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Criar;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Deletar;
using Microsoft.AspNetCore.Mvc;

namespace GateAPI.Controllers.Configuracao
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoLacreController(
        ILogger<TipoLacreController> logger,
        BuscarPorIdTipoLacreHandler buscarPorIdTipoLacreHandler,
        CriarTipoLacreHandler criarTipoLacreHandler,
        AtualizarTipoLacreHandler atualizarTipoLacreHandler,
        DeletarTipoLacreHandler deletarTipoLacreHandler
        ) : BaseController(logger)
    {
        private readonly BuscarPorIdTipoLacreHandler _buscarPorIdTipoLacreHandler = buscarPorIdTipoLacreHandler;
        private readonly CriarTipoLacreHandler _criarTipoLacreHandler = criarTipoLacreHandler;
        private readonly AtualizarTipoLacreHandler _atualizarTipoLacreHandler = atualizarTipoLacreHandler;
        private readonly DeletarTipoLacreHandler _deletarTipoLacreHandler = deletarTipoLacreHandler;

        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar([FromRoute] Guid id)
        {
            var query = new BuscarPorIdTipoLacreQuery(id);

            var result = await _buscarPorIdTipoLacreHandler.HandleAsync(query);

            return result.IsSuccess ? CreatedResponse(nameof(Criar), result.Data) : BadRequestResponse(result.Error ?? "Erro ao buscar lacre");
        }

        [HttpPost] 
        public async Task<IActionResult> Criar([FromBody] CriarTipoLacreCommand command)
        {
            var result = await _criarTipoLacreHandler.HandleAsync(command);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao criar lacre");
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarTipoLacreCommand command)
        {
            var result = await _atualizarTipoLacreHandler.HandleAsync(command);

            return result.IsSuccess ? NoContentResponse("Sucesso ao atualizar lacre") : BadRequestResponse(result.Error ?? "Erro ao atualizar lacre");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar([FromRoute] Guid Id)
        {
            var query = new DeletarTipoLacreCommand(Id);

            var result = await _deletarTipoLacreHandler.HandleAsync(query);

            return result.IsSuccess ? NoContentResponse("Scesso ao deletar lacre") : BadRequestResponse(result.Error ?? "Erro ao deletar lacre");
        }
    }
}
