using GateAPI.Application.UseCases.Integracao.ReconhecerPlacaUC;
using GateAPI.Requests.Integracao;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GateAPI.Controllers.Integracoes
{
    [Route("api/adapters")]
    [ApiController]
    public class AdaptersController(
        ILogger<AdaptersController> logger,
        IMediator mediator
        ) : BaseController(logger)
    {
        [HttpPost("/lpr/ler-placa")]
        public async Task<IActionResult> Criar([FromBody] LprRequest data)
        {
            var command = new ReconhecerPlacaCommand(data.ImagemPlaca);

            var result = await mediator.Send(command);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao reconhecer placa");
        }
    }
}
