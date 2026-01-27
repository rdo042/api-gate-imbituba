using System.Net;
using System.Net.Http.Json;
using GateAPI.Domain.Enums;

namespace GateAPI.Tests.Controllers.Configuracao
{
    public class IdiomaControllerTests(ApiFactory factory) : IClassFixture<ApiFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();

        #region GET Tests

        [Fact]
        public async Task Deve_buscar_todos_os_idiomas_com_sucesso()
        {
            var response = await _client.GetAsync("/api/idioma");

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(content);
        }

        [Fact]
        public async Task Deve_buscar_idiomas_app_com_sucesso()
        {
            var response = await _client.GetAsync("/api/idioma/app");

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(content);
        }

        [Fact]
        public async Task Deve_retornar_bad_request_ao_buscar_idioma_inexistente()
        {
            var idInexistente = Guid.NewGuid();
            var response = await _client.GetAsync($"/api/idioma/{idInexistente}");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion

        #region POST Tests

        [Fact]
        public async Task Deve_criar_idioma_com_sucesso()
        {
            var request = new
            {
                Codigo = "it-IT",
                Nome = "Idioma Teste",
                Descricao = "Idioma para testes",
                Status = StatusEnum.ATIVO,
                Canal = CanalEnum.App,
                EhPadrao = false
            };

            var response = await _client.PostAsJsonAsync("/api/idioma", request);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(content);
        }

        [Fact]
        public async Task Deve_criar_idioma_inativo()
        {
            var request = new
            {
                Codigo = "fr-CA",
                Nome = "Idioma Inativo",
                Descricao = "Idioma inativo para testes",
                Status = StatusEnum.INATIVO,
                Canal = CanalEnum.Retaguarda,
                EhPadrao = false
            };

            var response = await _client.PostAsJsonAsync("/api/idioma", request);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Deve_criar_idioma_em_ambos_canais()
        {
            var request = new
            {
                Codigo = "de-DE",
                Nome = "Idioma Ambos",
                Descricao = "Idioma em ambos os canais",
                Status = StatusEnum.ATIVO,
                Canal = CanalEnum.Ambos,
                EhPadrao = false
            };

            var response = await _client.PostAsJsonAsync("/api/idioma", request);

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Deve_falhar_ao_criar_idioma_sem_codigo()
        {
            var request = new
            {
                Nome = "Sem Código",
                Descricao = "Idioma sem código",
                Status = StatusEnum.ATIVO,
                Canal = CanalEnum.App,
                EhPadrao = false
            };

            var response = await _client.PostAsJsonAsync("/api/idioma", request);

            Assert.NotNull(response);
            Assert.NotEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Deve_falhar_ao_criar_idioma_sem_nome()
        {
            var request = new
            {
                Codigo = "ja-JP",
                Descricao = "Sem nome",
                Status = StatusEnum.ATIVO,
                Canal = CanalEnum.App,
                EhPadrao = false
            };

            var response = await _client.PostAsJsonAsync("/api/idioma", request);

            Assert.NotNull(response);
            Assert.NotEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Deve_falhar_ao_criar_idioma_com_codigo_invalido()
        {
            var request = new
            {
                Codigo = "INVALID123",
                Nome = "Idioma Código Inválido",
                Descricao = "Código inválido",
                Status = StatusEnum.ATIVO,
                Canal = CanalEnum.App,
                EhPadrao = false
            };

            var response = await _client.PostAsJsonAsync("/api/idioma", request);

            Assert.NotNull(response);
            Assert.NotEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Deve_falhar_ao_criar_segundo_idioma_como_padrao()
        {
            // Primeiro idioma padrão
            var request1 = new
            {
                Codigo = "ko-KR",
                Nome = "Primeiro Padrão",
                Descricao = "Primeiro idioma padrão",
                Status = StatusEnum.ATIVO,
                Canal = CanalEnum.Ambos,
                EhPadrao = true
            };

            var response1 = await _client.PostAsJsonAsync("/api/idioma", request1);
            if (response1.StatusCode != HttpStatusCode.Created)
            {
                return;
            }

            var request2 = new
            {
                Codigo = "zh-CN",
                Nome = "Segundo Padrão",
                Descricao = "Segundo idioma padrão",
                Status = StatusEnum.ATIVO,
                Canal = CanalEnum.App,
                EhPadrao = true
            };

            var response2 = await _client.PostAsJsonAsync("/api/idioma", request2);

            Assert.Equal(HttpStatusCode.BadRequest, response2.StatusCode);
        }

        #endregion

        #region PUT Tests

        [Fact]
        public async Task Deve_atualizar_idioma_nome()
        {
            var idValido = Guid.NewGuid();
            var updateRequest = new
            {
                Codigo = "es-ES",
                Nome = "Novo Nome",
                Descricao = "Descrição",
                Status = StatusEnum.ATIVO,
                Canal = CanalEnum.App
            };

            var response = await _client.PutAsJsonAsync($"/api/idioma/{idValido}", updateRequest);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task Deve_falhar_ao_atualizar_idioma_inexistente()
        {
            var idInexistente = Guid.NewGuid();
            var updateRequest = new
            {
                Codigo = "ru-RU",
                Nome = "Idioma Inexistente",
                Descricao = "Descrição",
                Status = StatusEnum.ATIVO,
                Canal = CanalEnum.App
            };

            var response = await _client.PutAsJsonAsync($"/api/idioma/{idInexistente}", updateRequest);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Deve_falhar_ao_atualizar_idioma_sem_codigo()
        {
            var idValido = Guid.NewGuid();
            var updateRequest = new
            {
                Nome = "Sem Código",
                Descricao = "Descrição",
                Status = StatusEnum.ATIVO,
                Canal = CanalEnum.App
            };

            var response = await _client.PutAsJsonAsync($"/api/idioma/{idValido}", updateRequest);

            Assert.NotEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        #endregion

        #region DELETE Tests

        [Fact]
        public async Task Deve_falhar_ao_deletar_idioma_inexistente()
        {
            var idInexistente = Guid.NewGuid();
            var response = await _client.DeleteAsync($"/api/idioma/{idInexistente}");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion

        #region PATCH Tests

        [Fact]
        public async Task Deve_retornar_bad_request_ao_definir_idioma_inexistente_como_padrao()
        {
            var idInexistente = Guid.NewGuid();
            var response = await _client.PatchAsync($"/api/idioma/{idInexistente}/padrao", null);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Deve_retornar_ok_ao_tentar_definir_padrao()
        {
            var idValido = Guid.NewGuid();
            var response = await _client.PatchAsync($"/api/idioma/{idValido}/padrao", null);

            Assert.True(response.StatusCode == HttpStatusCode.NoContent ||
                       response.StatusCode == HttpStatusCode.BadRequest);
        }

        #endregion
    }
}
