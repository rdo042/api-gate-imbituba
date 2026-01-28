using GateAPI.Application.UseCases.Configuracao.MotoristaUC.Atualizar;
using GateAPI.Application.UseCases.Configuracao.MotoristaUC.AtualizarParcial;
using GateAPI.Application.UseCases.Configuracao.MotoristaUC.BuscarPorDocumento;
using GateAPI.Application.UseCases.Configuracao.MotoristaUC.BuscarTodos;
using GateAPI.Application.UseCases.Configuracao.MotoristaUC.Criar;
using GateAPI.Application.UseCases.Configuracao.MotoristaUC.Deletar;
using GateAPI.Application.UseCases.Configuracao.TipoAvariaUC;
using GateAPI.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GateAPI.Controllers.Configuracao
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoristaController(
        ILogger<MotoristaController> logger,
         IMediator mediator
        ) : BaseController(logger)
    {

        [HttpGet]
        public async Task<IActionResult> BuscarTodos(PaginacaoRequest request)
        {
            var query = new BuscarTodosMotoristaQuery(request.PageNumber, request.PageSize);

            var result = await mediator.Send(query);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao buscar motorista");
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> BuscarPorId([FromRoute] Guid id)
        {
            //var query = new BuscarPorIdMotoristaQuery(id);
            //var result = await mediator.Send(query);

            var result = await mediator.Send(new BuscarPorIdMotoristaQuery(id));
            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao buscar motorista");
        }

        [HttpGet("{documento}")]
        public async Task<IActionResult> BuscarPorDocumento([FromRoute] string documento)
        {
            var query = new BuscarPorDocumentoMotoristaQuery(documento);
            var result = await mediator.Send(query);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao buscar motorista");
        }

        [HttpPost] 
        public async Task<IActionResult> Criar([FromBody] CriarMotoristaCommand command)
        {
            var result = await mediator.Send(command);
            return result.IsSuccess ? CreatedResponse(nameof(Criar),result.Data) : BadRequestResponse(result.Error ?? "Erro ao criar motorista");
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(ChangeBaseRequest<AtualizarMotoristaCommand> request)
        {
            if(request.id != request.Data.Id)
            {
                return BadRequestResponse("O id da rota difere do id do corpo da requisição");
            }

            var command = request.Data;

            var result = await mediator.Send(command);
      
            return result.IsSuccess ? NoContentResponse("Sucesso ao atualizar motorista") : BadRequestResponse(result.Error ?? "Erro ao atualizar motorista");
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar([FromRoute] Guid id)
        {
            var query = new DeletarMotoristaCommand(id);


            var result = await mediator.Send(query);

            return result.IsSuccess ? NoContentResponse("Sucesso ao deletar motorista") : BadRequestResponse(result.Error ?? "Erro ao deletar motorista");
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> AtualizarParcial(ChangeBaseRequest<AtualizarStatusMotoristaRequest> request)
        {
            var command = new AtualizarStatusMotoristaCommand(request.id, request.Data.Status);

            var result = await mediator.Send(command);

            return result.IsSuccess ? NoContentResponse("Sucesso ao atualizar motorista") : BadRequestResponse(result.Error ?? "Erro ao atualizar motorista");
        }

    }
}
