using GateAPI.Application.Providers;
using GateAPI.Domain.Entities.Integracao;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace GateAPI.Infra.Providers
{
    public class ExternalLprProvider(
        HttpClient http,
        ILogger<ExternalLprProvider> logger) : ILprProvider
    {
        private readonly HttpClient _http = http;
        private readonly ILogger<ExternalLprProvider> _logger = logger;

        public async Task<Lpr?> RecognizeAsync(
            string base64,
            CancellationToken ct)
        {
            try
            {
                //var response = await _http.PostAsJsonAsync(
                //    "api/Ocr/ProcessVehiclePlate",
                //    new { image = base64 },
                //    ct);

                //if (!response.IsSuccessStatusCode)
                //    return null;

                //var result = await response.Content
                //    .ReadFromJsonAsync<Lpr>(ct);

                var result = new Lpr("OZL7H33", "2025-12-01T11:45:06.2000421-03:00", "2025-12-01T11:45:07.7182738-03:00");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro no LPR externo");
                return null;
            }
        }
    }
}
