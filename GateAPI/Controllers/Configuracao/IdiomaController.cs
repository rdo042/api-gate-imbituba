using GateAPI.Application.UseCases.Configuracao.IdiomaUC.Atualizar;
using GateAPI.Application.UseCases.Configuracao.IdiomaUC.BuscarIdiomaApp;
using GateAPI.Application.UseCases.Configuracao.IdiomaUC.BuscarPorId;
using GateAPI.Application.UseCases.Configuracao.IdiomaUC.BuscarTodos;
using GateAPI.Application.UseCases.Configuracao.IdiomaUC.Criar;
using GateAPI.Application.UseCases.Configuracao.IdiomaUC.Deletar;
using GateAPI.Application.UseCases.Configuracao.IdiomaUC.DefinirPadrao;
using GateAPI.Requests.Configuracao.IdiomaRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GateAPI.Controllers.Configuracao
{
    [Route("api/idioma")]
    [ApiController]
    public class IdiomaController(
        ILogger<IdiomaController> logger,
        IMediator mediator
    ) : BaseController(logger)
    {
        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var query = new BuscarTodosIdiomaQuery();

            var result = await mediator.Send(query);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao buscar idiomas");
        }

        [HttpGet("app")]
        public async Task<IActionResult> BuscarIdiomaApp()
        {
            var query = new BuscarIdiomaAppQuery();

            var result = await mediator.Send(query);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao buscar idiomas do app");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId([FromRoute] Guid id)
        {
            var query = new BuscarPorIdIdiomaQuery(id);

            var result = await mediator.Send(query);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Idioma não encontrado");
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarIdiomaRequest data)
        {
            var command = new CriarIdiomaCommand(
                data.Codigo,
                data.Nome,
                data.Descricao,
                data.Status,
                data.Canal,
                data.EhPadrao
            );

            var result = await mediator.Send(command);

            return result.IsSuccess ? CreatedResponse(nameof(Criar), result.Data) : BadRequestResponse(result.Error ?? "Erro ao criar idioma");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar([FromRoute] Guid id, [FromBody] AtualizarIdiomaRequest data)
        {
            var command = new AtualizarIdiomaCommand(
                id,
                data.Codigo,
                data.Nome,
                data.Descricao,
                data.Status,
                data.Canal
            );

            var result = await mediator.Send(command);

            return result.IsSuccess ? NoContentResponse(result.Data ?? "Sucesso ao atualizar idioma") : BadRequestResponse(result.Error ?? "Erro ao atualizar idioma");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar([FromRoute] Guid id)
        {
            var command = new DeletarIdiomaCommand(id);

            var result = await mediator.Send(command);

            return result.IsSuccess ? NoContentResponse(result.Data ?? "Sucesso ao deletar idioma") : BadRequestResponse(result.Error ?? "Erro ao deletar idioma");
        }

        [HttpPatch("{id}/padrao")]
        public async Task<IActionResult> DefinirComoPadrao([FromRoute] Guid id)
        {
            var command = new DefinirIdiomasPadraoCommand(id);

            var result = await mediator.Send(command);

            return result.IsSuccess ? NoContentResponse(result.Data ?? "Sucesso ao definir idioma padrão") : BadRequestResponse(result.Error ?? "Erro ao definir idioma padrão");
        }
    }
}
