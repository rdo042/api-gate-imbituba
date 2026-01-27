using System.Net;
using System.Net.Http.Json;

namespace GateAPI.Tests.Controllers.Configuracao
{
    public class UsuarioControllerTests(ApiFactory factory) : IClassFixture<ApiFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task Deve_criar_usuario_com_sucesso()
        {
            var response = await _client.PostAsJsonAsync("/api/usuarios", new
            {
                Nome = "Teste"
            });

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
