using GateAPI.Application.UseCases.Configuracao.PerfilUC.Criar;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Criar;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.PerfilUC
{
    public class CriarPerfilHandlerTests
    {
        private readonly Mock<IPerfilRepository> _repositoryMock;
        private readonly CriarPerfilHandler _handler;

        public CriarPerfilHandlerTests()
        {
            _repositoryMock = new Mock<IPerfilRepository>();
            _handler = new CriarPerfilHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarSucesso_QuandoPerfilForCriado()
        {
            // Arrange
            var command = new CriarPerfilCommand(
                "ADMIN",
                "Perfil de Administrador",
                []
            );

            var criado = new Perfil(command.Nome, command.Descricao, []);

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Perfil>()))
                           .ReturnsAsync(criado);

            // Act: Executamos o Handler
            var result = await _handler.HandleAsync(command);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("ADMIN", result.Data.Nome);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Perfil>()), Times.Once);
        }

        [Theory]
        [InlineData("")] // Nome vazio
        public async Task HandleAsync_DeveFalhar_SeDadosForemInvalidos(string nome)
        {
            // Arrange
            var command = new CriarPerfilCommand(nome, null, []);

            // Act
            var result = await _handler.HandleAsync(command);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
