using GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.Atualizar;
using GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.BuscarPorId;
using GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.BuscarTodos;
using GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.BuscarTodosApp;
using GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.Criar;
using GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.Deletar;
using GateAPI.Requests.Configuracao.LocalAvariaRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GateAPI.Controllers.Configuracao
{
    [Route("api/local-avaria")]
    [ApiController]
    public class LocalAvariaController(
        ILogger<TipoLacreController> logger,
        IMediator mediator
        ) : BaseController(logger)
    {
        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var query = new BuscarTodosLocalAvariaQuery();

            var result = await mediator.Send(query);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Locais Avaria não encontrados");
        }

        [HttpGet("app")]
        public async Task<IActionResult> BuscarTodosApp()
        {
            var query = new BuscarTodosAppLocalAvariaQuery();

            var result = await mediator.Send(query);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Locais Avaria não encontrados");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar([FromRoute] Guid id)
        {
            var query = new BuscarPorIdLocalAvariaQuery(id);

            var result = await mediator.Send(query);

            return result.IsSuccess? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao buscar local avaria");
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarLocalAvariaRequest data)
        {
            var command = new CriarLocalAvariaCommand(data.Local, data.Descricao, data.Status);

            var result = await mediator.Send(command);

            return CreatedResponse(nameof(Criar), result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar([FromRoute] Guid id, [FromBody] AtualizarLocalAvariaRequest data)
        {
            var command = new AtualizarLocalAvariaCommand(id, data.Local, data.Descricao, data.Status);

            var result = await mediator.Send(command);

            return result.IsSuccess ? NoContentResponse("Sucesso ao atualizar local avaria") : BadRequestResponse(result.Error ?? "Erro ao atualizar local avaria");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar([FromRoute] Guid id)
        {
            var query = new DeletarLocalAvariaCommand(id);

            var result = await mediator.Send(query);

            return result.IsSuccess ? NoContentResponse("Sucesso ao deletar local avaria") : BadRequestResponse(result.Error ?? "Erro ao deletar local avaria");
        }
    }
}
