using GateAPI.Application.UseCases.Integracao.ReconhecerPlacaUC;
using GateAPI.Domain.Entities.Integracao;
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
        public async Task<IActionResult> LerPlaca([FromBody] LprRequest data)
        {
            var command = new ReconhecerPlacaCommand(data.ImagemPlaca);

            var result = await mediator.Send(command);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao reconhecer placa");
            //return OkResponse("Not implemented");
        }
        //tasks/obter-tasks?placa={placa}
        [HttpGet("tasks/obter-tasks")]
        public async Task<IActionResult> ObterTasksPorPlaca([FromQuery] string placa)
        {
            var command = new TaskPorPlacaCommand(placa);

            var result = await mediator.Send(command);

            return result.IsSuccess ? OkResponse(result.Data) : BadRequestResponse(result.Error ?? "Erro ao consultar task");
        }

        //driver/validar-foto-motorista?numeroDocumento={numero_cpf}
        [HttpGet("driver/validar-foto-motorista")]
        public async Task<IActionResult> ValidarFotoMotorista([FromQuery] string numeroDocumento)
        {
            var driverMock = new Driver
            {
                Name = "Carlos Silva",
                DateOfBirth = new DateTime(1990, 5, 20),
                IsValidDriver = true,
                Documents = new
                {
                    CnhNumber = "12345678901",
                    Category = "B",
                    IssuingState = "SP"
                },
                Base64FacialBiometrics = "RkFLRV9CQVNFMjZGX0ZBQ0lBTF9CSU9NRVRSSUNT",
                Base64Document = "RkFLRV9CQVNFMjZGX0RPQ1VNRU5UX0NOVA=="
            };


            return OkResponse(driverMock);
        }
    }
}
