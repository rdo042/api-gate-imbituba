using GateAPI.Application.UseCases.Configuracao.TaskFlowTasksUC.AlterarOrdem;
using GateAPI.Application.UseCases.Configuracao.TaskFlowTasksUC.Criar;
using GateAPI.Application.UseCases.Configuracao.TaskFlowTasksUC.Deletar;
using GateAPI.Application.UseCases.Configuracao.TaskFlowUC.Atualizar;
using GateAPI.Application.UseCases.Configuracao.TaskFlowUC.BuscarPorParametro;
using GateAPI.Application.UseCases.Configuracao.TaskFlowUC.BuscarTodos;
using GateAPI.Application.UseCases.Configuracao.TaskFlowUC.Criar;
using GateAPI.Application.UseCases.Configuracao.TaskFlowUC.Deletar;
using GateAPI.Application.UseCases.Configuracao.TasksUC.BuscarTodosPorParametro;
using GateAPI.Requests.Configuracao.TaskFlow;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GateAPI.Controllers.Configuracao
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskFlowController(
        ILogger<TaskFlowController> logger,
        IMediator mediator
        ) : BaseController(logger)
    {
        [HttpGet("buscar-todas-tasks")]
        public async Task<IActionResult> BuscarTodasTarefas()
        {
            var query = new BuscarTodosPorParametroTasksQuery(null);

            var result = await mediator.Send(query);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao buscar tasks");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar([FromRoute] Guid id)
        {
            var query = new BuscarPorParametroTaskFlowQuery(id);

            var result = await mediator.Send(query);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao buscar flow");
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var query = new BuscarTodosTaskFlowQuery();

            var result = await mediator.Send(query);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao buscar flow");
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] SalvarTaskFlowRequest data)
        {
            var query = new CriarTaskFlowCommand(data.Nome);

            var result = await mediator.Send(query);

            return result.IsSuccess ? CreatedResponse(nameof(Criar), result.Data) : BadRequestResponse(result.Error ?? "Erro ao criar flow");
        }

        [HttpPost("criar-relacao")]
        public async Task<IActionResult> CriarRelacao([FromBody] CriarTaskFlowTasksRequest data)
        {
            var query = new CriarTaskFlowTasksCommand(data.FlowId, data.TaskId);

            var result = await mediator.Send(query);

            return result.IsSuccess ? CreatedResponse(nameof(CriarRelacao), result.Data) : BadRequestResponse(result.Error ?? "Erro ao criar relacao");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar([FromRoute] Guid id, [FromBody] SalvarTaskFlowRequest data)
        {
            var query = new AtualizarTaskFlowCommand(id, data.Nome);

            var result = await mediator.Send(query);

            return result.IsSuccess ? NoContentResponse("Flow atualizado com sucesso") : BadRequestResponse(result.Error ?? "Erro ao atualizar flow");
        }

        [HttpPut("relacao-alterar-ordem/{flowId}")]
        public async Task<IActionResult> AtualizarRelacaoOrdem([FromRoute] Guid flowId, [FromBody] AlterarOrdemRelacaoRequest data)
        {
            var query = new AlterarOrdemTaskFlowTasksCommand(flowId, data.TasksId, data.Subir);

            var result = await mediator.Send(query);

            return result.IsSuccess ? NoContentResponse("Ordem alterada com sucesso") : BadRequestResponse(result.Error ?? "Erro ao atualizar relacao");
        }

        [HttpPut("relacao-alterar-status/{flowId}")]
        public async Task<IActionResult> AtualizarRelacaoStatus([FromRoute] Guid flowId, [FromBody] AlterarOrdemRelacaoRequest data)
        {
            var query = new AlterarOrdemTaskFlowTasksCommand(flowId, data.TasksId, data.Subir);

            var result = await mediator.Send(query);

            return result.IsSuccess ? NoContentResponse("Ordem alterada com sucesso") : BadRequestResponse(result.Error ?? "Erro ao atualizar relacao");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar([FromRoute] Guid id)
        {
            var query = new DeletarTaskFlowCommand(id);

            var result = await mediator.Send(query);

            return result.IsSuccess 
                ? NoContentResponse("Flow deletado com sucesso") 
                : BadRequestResponse(result.Error ?? "Erro ao deletar flow");
        }

        [HttpDelete("deletar-relacao/{flowId}/task/{taskId}")]
        public async Task<IActionResult> DeletarRelacao([FromRoute] Guid flowId, [FromRoute] Guid taskId)
        {
            var query = new DeletarTaskFlowTasksCommand(flowId,taskId);

            var result = await mediator.Send(query);

            return result.IsSuccess 
                ? NoContentResponse("Relacao deletado com sucesso") 
                : BadRequestResponse(result.Error ?? "Erro ao deletar relacao");
        }
    }
}
