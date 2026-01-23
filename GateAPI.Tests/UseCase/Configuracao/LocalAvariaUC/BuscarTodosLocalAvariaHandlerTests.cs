using GateAPI.Application.UseCases.Configuracao.LocalAvariaUC.BuscarTodos;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.LocalAvariaUC
{
    public class BuscarTodosLocalAvariaHandlerTests
    {
        private readonly Mock<ILocalAvariaRepository> _repositoryMock;
        private readonly BuscarTodosLocalAvariaHandler _handler;
        private readonly StatusEnum _validStatusEnum = StatusEnum.ATIVO;

        public BuscarTodosLocalAvariaHandlerTests()
        {
            _repositoryMock = new Mock<ILocalAvariaRepository>();
            _handler = new BuscarTodosLocalAvariaHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso_QuandoLocaisForemEncontrados()
        {
            // Arrange
            var command = new BuscarTodosLocalAvariaQuery();

            var locaisAvaria = new List<LocalAvaria>
            {
                new LocalAvaria("LOCAL001", "Descrição do local 1", _validStatusEnum),
                new LocalAvaria("LOCAL002", "Descrição do local 2", _validStatusEnum),
                new LocalAvaria("LOCAL003", "Descrição do local 3", _validStatusEnum)
            };

            _repositoryMock.Setup(r => r.GetAllAsync())
                           .ReturnsAsync(locaisAvaria);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(3, result.Data.Count());
            Assert.Contains(result.Data, x => x.Local == "LOCAL001");
            Assert.Contains(result.Data, x => x.Local == "LOCAL002");
            Assert.Contains(result.Data, x => x.Local == "LOCAL003");
            _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveFalhar_QuandoNenhumEncontrado()
        {
            // Arrange
            var command = new BuscarTodosLocalAvariaQuery();

            var locaisVazios = new List<LocalAvaria>();

            _repositoryMock.Setup(r => r.GetAllAsync())
                           .ReturnsAsync(locaisVazios);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Erro ao buscar", result.Error);
            _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
