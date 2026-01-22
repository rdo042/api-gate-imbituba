using GateAPI.Application.UseCases.Configuracao.TipoAvariaUC;
using GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Criar;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Atualizar;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GateAPI.Controllers.Configuracao
{
    //[Route("api/[controller]")]
    [Route("api/tipo-avaria")]
    [ApiController]
    public class TipoAvariaController(
        ILogger<TipoAvariaController> logger,
         IMediator mediator
        ) : BaseController(logger)
    {
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar([FromRoute] Guid id)
        {
            var query = new BuscarPorIdTipoAvariaQuery(id);
            var result = await mediator.Send(query);

            return result.IsSuccess ? CreatedResponse(nameof(Criar), result.Data) : BadRequestResponse(result.Error ?? "Erro ao buscar avaria");
        }

        [HttpPost] 
        public async Task<IActionResult> Criar([FromBody] CriarTipoAvariaRequest request)
        {
            var command = new CriarTipoAvariaCommand { Descricao = request.Descricao, Status = request.Status, Tipo = request.Tipo };

            var result = await mediator.Send(command);
            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao criar avaria");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(AlterarTipoAvariaRequest request)
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
