using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Atualizar;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.BuscarPorId;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Criar;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Deletar;
using GateAPI.Requests.Configuracao.TipoLacreRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GateAPI.Controllers.Configuracao
{
    [Route("api/tipo-lacre")]
    [ApiController]
    public class TipoLacreController(
        ILogger<TipoLacreController> logger,
        IMediator mediator
        ) : BaseController(logger)
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar([FromRoute] Guid id)
        {
            var query = new BuscarPorIdTipoLacreQuery(id);

            var result = await mediator.Send(query);

            return result.IsSuccess? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao buscar lacre");
        }

        [HttpPost] 
        public async Task<IActionResult> Criar([FromBody] CriarTipoLacreRequest data)
        {
            var command = new CriarTipoLacreCommand(data.Nome, data.Descricao, data.Status);

            var result = await mediator.Send(command);

            return CreatedResponse(nameof(Criar), result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar([FromRoute] Guid id, [FromBody] AtualizarTipoLacreRequest data)
        {
            var command = new AtualizarTipoLacreCommand(id, data.Nome, data.Descricao, data.Status);

            var result = await mediator.Send(command);

            return result.IsSuccess ? NoContentResponse("Sucesso ao atualizar lacre") : BadRequestResponse(result.Error ?? "Erro ao atualizar lacre");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar([FromRoute] Guid Id)
        {
            var query = new DeletarTipoLacreCommand(Id);

            var result = await mediator.Send(query);

            return result.IsSuccess ? NoContentResponse("Scesso ao deletar lacre") : BadRequestResponse(result.Error ?? "Erro ao deletar lacre");
        }
    }
}
