using GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.BuscarTodosApp;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.LocalAvariaUC
{
    public class BuscarTodosAppLocalAvariaHandlerTests
    {
        private readonly Mock<ILocalAvariaRepository> _repositoryMock;
        private readonly BuscarTodosAppLocalAvariaHandler _handler;
        private readonly StatusEnum _validStatusEnum = StatusEnum.ATIVO;
        private readonly StatusEnum _inactiveStatusEnum = StatusEnum.INATIVO;

        public BuscarTodosAppLocalAvariaHandlerTests()
        {
            _repositoryMock = new Mock<ILocalAvariaRepository>();
            _handler = new BuscarTodosAppLocalAvariaHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso_QuandoLocaisAtivosForemEncontrados()
        {
            // Arrange
            var command = new BuscarTodosAppLocalAvariaQuery();

            var locaisAtivos = new List<LocalAvaria>
            {
                new LocalAvaria("LOCAL001", "Descrição do local 1", _validStatusEnum),
                new LocalAvaria("LOCAL002", "Descrição do local 2", _validStatusEnum),
                new LocalAvaria("LOCAL003", "Descrição do local 3", _validStatusEnum)
            };

            _repositoryMock.Setup(r => r.GetAllAppAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync(locaisAtivos);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(3, result.Data.Count());
            Assert.All(result.Data, x => Assert.Equal(_validStatusEnum, x.Status));
            _repositoryMock.Verify(r => r.GetAllAppAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveFalhar_QuandoNenhumAtivoEncontrado()
        {
            // Arrange
            var command = new BuscarTodosAppLocalAvariaQuery();

            var locaisVazios = new List<LocalAvaria>();

            _repositoryMock.Setup(r => r.GetAllAppAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync(locaisVazios);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Erro ao buscar", result.Error);
            _repositoryMock.Verify(r => r.GetAllAppAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveFalhar_QuandoTodosEstaoInativos()
        {
            // Arrange
            var command = new BuscarTodosAppLocalAvariaQuery();

            var locaisInativos = new List<LocalAvaria>
            {
                new LocalAvaria("LOCAL001", "Descrição do local 1", _inactiveStatusEnum),
                new LocalAvaria("LOCAL002", "Descrição do local 2", _inactiveStatusEnum)
            };

            _repositoryMock.Setup(r => r.GetAllAppAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync(new List<LocalAvaria>());

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Erro ao buscar", result.Error);
            _repositoryMock.Verify(r => r.GetAllAppAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
