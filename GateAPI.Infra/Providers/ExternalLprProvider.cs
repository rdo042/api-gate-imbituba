using GateAPI.Application.Providers;
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

        public async Task<string?> RecognizeAsync(
            string base64,
            CancellationToken ct)
        {
            try
            {
                var response = await _http.PostAsJsonAsync(
                    "/lpr/recognize",
                    new { image = base64 },
                    ct);

                if (!response.IsSuccessStatusCode)
                    return null;

                var dto = await response.Content
                    .ReadFromJsonAsync<string>(ct);

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro no LPR externo");
                return null;
            }
        }
    }
}
