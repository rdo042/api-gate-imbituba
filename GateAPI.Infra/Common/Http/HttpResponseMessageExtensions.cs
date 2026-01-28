namespace GateAPI.Infra.Common.Http
{
    using GateAPI.Application.Common.Exceptions;
    using System.Net;

    public static class HttpResponseMessageExtensions
    {
        public static async Task ThrowIfErrorAsync(this HttpResponseMessage response, string serviceName)
        {
            if (response.IsSuccessStatusCode)
                return;

            var body = await response.Content.ReadAsStringAsync();

            throw response.StatusCode switch
            {
                HttpStatusCode.BadRequest =>
                    new ExternalServiceBadRequestException(
                        $"{serviceName}: requisição inválida. {body}"),

                HttpStatusCode.Unauthorized =>
                    new ExternalServiceUnauthorizedException(
                        $"{serviceName}: não autorizado."),

                HttpStatusCode.Forbidden =>
                    new ExternalServiceForbiddenException(
                        $"{serviceName}: acesso negado."),

                HttpStatusCode.NotFound =>
                    new ExternalServiceNotFoundException(
                        $"{serviceName}: recurso não encontrado."),

                HttpStatusCode.InternalServerError =>
                    new ExternalServiceInternalErrorException(
                        $"{serviceName}: erro interno."),

                HttpStatusCode.ServiceUnavailable =>
                    new ExternalServiceUnavailableException(
                        $"{serviceName}: serviço indisponível."),

                _ =>
                    new ExternalServiceException($"{serviceName}: erro inesperado ({(int)response.StatusCode}). Body: {body}")
            };
        }
    }

}
