using System.Net;
using System.Net.Http.Json;

namespace GateAPI.Tests.Controllers.Configuracao
{
    public class TipoLacreControllerTests(ApiFactory factory) : IClassFixture<ApiFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task Deve_criar_tipoLacre_com_sucesso()
        {
            var response = await _client.PostAsJsonAsync("/api/tipo-lacre", new
            {
                Nome = "LAC003",
                Descricao = "lacre operacional",
                Status = "ATIVO"
        });

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
