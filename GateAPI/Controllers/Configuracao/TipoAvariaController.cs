using GateAPI.Application.Common.Models;
using GateAPI.Application.UseCases.Configuracao.TipoAvariaUC;
using GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.AtualizarParcial;
using GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Criar;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Deletar;
using GateAPI.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GateAPI.Controllers.Configuracao
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAvariaController(
        ILogger<TipoAvariaController> logger,
         IMediator mediator
        ) : BaseController(logger)
    {

        [HttpGet]
        public async Task<IActionResult> BuscarTodos(PaginacaoRequest request)
        {
            var query = new BuscarTodosTipoAvariaQuery(request.PageNumber, request.PageSize);

            var result = await mediator.Send(query);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao buscar avaria");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar([FromRoute] Guid id)
        {
            var query = new BuscarPorIdTipoAvariaQuery(id);
            var result = await mediator.Send(query);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao buscar avaria");
        }

        [HttpPost] 
        public async Task<IActionResult> Criar([FromBody] CriarTipoAvariaRequest request)
        {
            var command = new CriarTipoAvariaCommand(request.Tipo, request.Descricao, request.Status);

            var result = await mediator.Send(command);
            return result.IsSuccess ? CreatedResponse(nameof(Criar),result.Data) : BadRequestResponse(result.Error ?? "Erro ao criar avaria");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(ChangeBaseRequest<AtualizarTipoAvariaRequest> request)
        {
            var command = new AtualizarTipoAvariaCommand(request.id, request.Data.Tipo, request.Data.Descricao, request.Data.Status);
            var result = await mediator.Send(command);
      
            return result.IsSuccess ? NoContentResponse("Sucesso ao atualizar avaria") : BadRequestResponse(result.Error ?? "Erro ao atualizar avaria");
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar([FromRoute] Guid id)
        {
            var query = new DeletarTipoAvariaCommand(id);

            var result = await mediator.Send(query);

            return result.IsSuccess ? NoContentResponse("Sucesso ao deletar avaria") : BadRequestResponse(result.Error ?? "Erro ao deletar avaria");
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> AtualizarParcial(ChangeBaseRequest<AtualizarParcialTipoAvariaRequest> request)
        {
            var command = new AtualizarParcialTipoAvariaCommand(request.id, request.Data.Tipo, request.Data.Descricao, request.Data.Status);
            var result = await mediator.Send(command);

            return result.IsSuccess ? NoContentResponse("Sucesso ao atualizar avaria") : BadRequestResponse(result.Error ?? "Erro ao atualizar avaria");
        }

    }
}
