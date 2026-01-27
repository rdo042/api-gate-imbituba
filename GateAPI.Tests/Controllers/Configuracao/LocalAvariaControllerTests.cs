using System.Net;
using System.Net.Http.Json;
using GateAPI.Domain.Enums;

namespace GateAPI.Tests.Controllers.Configuracao
{
    public class LocalAvariaControllerTests(ApiFactory factory) : IClassFixture<ApiFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();

        #region GET Tests

        [Fact]
        public async Task Deve_buscar_todos_os_locais_avaria_com_sucesso()
        {
            var response = await _client.GetAsync("/api/local-avaria");

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(content);
        }

        [Fact]
        public async Task Deve_buscar_locais_avaria_app_com_sucesso()
        {
            var response = await _client.GetAsync("/api/local-avaria/app");

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(content);
        }

        [Fact]
        public async Task Deve_retornar_not_found_ao_buscar_local_avaria_inexistente()
        {
            var idInexistente = Guid.NewGuid();
            var response = await _client.GetAsync($"/api/local-avaria/{idInexistente}");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion

        #region POST Tests

        [Fact]
        public async Task Deve_criar_local_avaria_com_sucesso()
        {
            var request = new
            {
                Local = "AVARIA_TESTE_" + Guid.NewGuid().ToString().Substring(0, 8),
                Descricao = "Local de avaria para testes",
                Status = StatusEnum.ATIVO
            };

            var response = await _client.PostAsJsonAsync("/api/local-avaria", request);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(content);
        }

        [Fact]
        public async Task Deve_criar_local_avaria_inativo()
        {
            var request = new
            {
                Local = "AVARIA_INATIVO_" + Guid.NewGuid().ToString().Substring(0, 8),
                Descricao = "Local de avaria inativo",
                Status = StatusEnum.INATIVO
            };

            var response = await _client.PostAsJsonAsync("/api/local-avaria", request);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Deve_falhar_ao_criar_local_avaria_sem_local()
        {
            var request = new
            {
                Descricao = "Local de avaria sem nome",
                Status = StatusEnum.ATIVO
            };

            var response = await _client.PostAsJsonAsync("/api/local-avaria", request);

            Assert.NotNull(response);
            Assert.NotEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Deve_falhar_ao_criar_local_avaria_sem_descricao()
        {
            var request = new
            {
                Local = "AVARIA_TESTE",
                Status = StatusEnum.ATIVO
            };

            var response = await _client.PostAsJsonAsync("/api/local-avaria", request);

            Assert.NotNull(response);
            Assert.NotEqual(HttpStatusCode.Created, response.StatusCode);
        }

        #endregion

        #region PUT Tests

        [Fact]
        public async Task Deve_falhar_ao_atualizar_local_avaria_inexistente()
        {
            var idInexistente = Guid.NewGuid();
            var updateRequest = new
            {
                Local = "AVARIA_TESTE",
                Descricao = "Descrição",
                Status = StatusEnum.ATIVO
            };

            var response = await _client.PutAsJsonAsync($"/api/local-avaria/{idInexistente}", updateRequest);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Deve_falhar_ao_atualizar_local_avaria_sem_local()
        {
            var idValido = Guid.NewGuid();
            var updateRequest = new
            {
                Descricao = "Descrição",
                Status = StatusEnum.ATIVO
            };

            var response = await _client.PutAsJsonAsync($"/api/local-avaria/{idValido}", updateRequest);

            Assert.NotEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        #endregion

        #region DELETE Tests

        [Fact]
        public async Task Deve_falhar_ao_deletar_local_avaria_inexistente()
        {
            var idInexistente = Guid.NewGuid();
            var response = await _client.DeleteAsync($"/api/local-avaria/{idInexistente}");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion
    }
}
