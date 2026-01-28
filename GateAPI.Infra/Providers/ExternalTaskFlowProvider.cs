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