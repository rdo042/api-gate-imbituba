using GateAPI.Application.Common.Exceptions;
using GateAPI.Application.Common.Models;
using GateAPI.Application.Providers;
using GateAPI.Domain.Entities.Integracao;
using GateAPI.Infra.Common.Http;
using GateAPI.Infra.Providers.Configuracao;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace GateAPI.Infra.Providers
{
    public class ExternalTaskFlowProvider : IExternalTaskFlowProvider
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly ExternalApiOptions _options;

        public ExternalTaskFlowProvider(HttpClient httpClient, IOptions<ExternalApiOptions> options)
        {
            _options = options.Value;
            _httpClient = httpClient;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<TaskFlowPlateResponse> GetTasksByLicensePlateAsync(
            string licensePlate,
            CancellationToken cancellationToken)
        {
            try
            {
                if (_options.IsMock)
                {
                    // Retorna dados mockados para testes
                    return new TaskFlowPlateResponse
                    {
                        Header = new Header
                        {
                            ProductId = 1,
                            Version = "1.0",
                            DateTimeGeneration = DateTimeOffset.Now,
                            SystemOrigin = "SYSTEM_TEST",
                            TransactionId = "TX123456",
                            TypeMessage = "RESPONSE",
                            Company = "MinhaEmpresa",
                            Flow = "ENTRADA",
                            Checkpoint = new Checkpoint
                            {
                                Id = 10,
                                Description = "Portaria Principal"
                            }
                        },
                        Errors = new List<object>(),

                        OcrVehiclePlate = licensePlate,//"ABC1D23",

                        Driver = null, // ou um objeto fake se depois tipar isso melhor

                        VehiclePassages = new List<VehiclePassage>
                        {
                            new VehiclePassage
                            {
                                ProductId = 1,
                                SchedulingId = 999,
                                Mission = "Carga",
                                CurrentStatusPassage = "AGENDADO",
                                PlannedDate = DateTime.Today,
                                Terminal = "Terminal A",
                                LicensePlate = "ABC1D23",
                                EntryTime = null,
                                ExitTime = null,
                                OuterEntryTime = null,
                                DateTimeInspection = null,
                                VehicleType = "Caminhão",
                                IdentifierTag = "TAG123",
                                Schedules = new List<Schedule>
                                {
                                    new Schedule
                                    {
                                        Id = 1,
                                        TrailerLicensePlate = "DEF4G56",
                                        Container = "CONT1234567"
                                    }
                                }
                            }
                        }
                    };
                }


                _httpClient.BaseAddress = new Uri(_options.BaseUrl);

                var url = $"api/TaskFlow/GetTasksByLicensePlate?licensePlate={licensePlate}";

                using var response = await _httpClient.GetAsync(url, cancellationToken);

                await response.ThrowIfErrorAsync("TaskFlow API");

                var content = await response.Content.ReadAsStringAsync(cancellationToken);

                if (string.IsNullOrWhiteSpace(content))
                    throw new ExternalServiceException("TaskFlow API: resposta vazia.");

                var result = JsonSerializer.Deserialize<TaskFlowPlateResponse>(content, _jsonOptions);

                return result
                    ?? throw new ExternalServiceException("TaskFlow API: falha ao desserializar resposta.");
            }
            catch (TaskCanceledException)
            {
                throw new ExternalServiceUnavailableException(
                    "TaskFlow API: timeout ao tentar comunicação.");
            }
            catch (HttpRequestException ex)
            {
                throw new ExternalServiceUnavailableException(
                    $"TaskFlow API: erro de comunicação. {ex.Message}");
            }
        }
    }
}