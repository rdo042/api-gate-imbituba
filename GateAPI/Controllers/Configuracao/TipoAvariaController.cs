using GateAPI.Application.UseCases.Configuracao.TipoAvariaUC;
using GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Criar;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Atualizar;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.BuscarPorId;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Criar;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Deletar;
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

            return result.IsSuccess ? CreatedResponse(nameof(Criar), result.Data) : BadRequestResponse(result.Error ?? "Erro ao buscar lacre");
        }

        [HttpPost] 
        public async Task<IActionResult> Criar([FromBody] CriarTipoAvariaRequest request)
        {
            var mappingCmd = new Domain.Entities.Configuracao.TipoAvaria(request.Tipo, request.Descricao, request.Status);

            var command = new CriarTipoAvariaCommand(mappingCmd);


            var result = await mediator.Send(command);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao criar avaria");
        }

        //[HttpPut]
        //public async Task<IActionResult> Atualizar([FromBody] AtualizarTipoLacreCommand command)
        //{
        //    var result = await _atualizarTipoLacreHandler.HandleAsync(command);

        //    return result.IsSuccess ? NoContentResponse("Sucesso ao atualizar lacre") : BadRequestResponse(result.Error ?? "Erro ao atualizar lacre");
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Deletar([FromRoute] Guid Id)
        //{
        //    var query = new DeletarTipoLacreCommand(Id);

        //    var result = await _deletarTipoLacreHandler.HandleAsync(query);

        //    return result.IsSuccess ? NoContentResponse("Scesso ao deletar lacre") : BadRequestResponse(result.Error ?? "Erro ao deletar lacre");
        //}
    }
}
