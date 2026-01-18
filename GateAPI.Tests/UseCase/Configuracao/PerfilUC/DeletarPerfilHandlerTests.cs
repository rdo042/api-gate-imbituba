using GateAPI.Application.UseCases.Configuracao.PerfilUC.Deletar;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Deletar;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateAPI.Tests.UseCase.Configuracao.PerfilUC
{
    public class DeletarPerfilHandlerTests
    {
        private readonly Mock<IPerfilRepository> _repositoryMock;
        private readonly DeletarPerfilHandler _handler;

        public DeletarPerfilHandlerTests()
        {
            _repositoryMock = new Mock<IPerfilRepository>();
            _handler = new DeletarPerfilHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarSucesso_QuandoPerfilForDeletado()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var command = new DeletarPerfilCommand(guid);

            _repositoryMock.Setup(r => r.DeleteAsync(guid))
                           .Returns(Task.CompletedTask);

            // Act: Executamos o Handler
            var result = await _handler.HandleAsync(command);

            Assert.True(result.IsSuccess);
            _repositoryMock.Verify(r => r.DeleteAsync(guid), Times.Once);
        }
    }
}
