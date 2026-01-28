using GateAPI.Application.UseCases.Configuracao.TaskFlowUC.Deletar;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.TaskFlowUC
{
    public class DeletarTaskFlowHandlerTests
    {
        private readonly Mock<ITaskFlowRepository> _repositoryMock;
        private readonly Mock<ITaskFlowTasksRepository> _repositoryMock2;
        private readonly DeletarTaskFlowHandler _handler;
        public DeletarTaskFlowHandlerTests()
        {
            _repositoryMock = new Mock<ITaskFlowRepository>();
            _repositoryMock2 = new Mock<ITaskFlowTasksRepository>();
            _handler = new DeletarTaskFlowHandler(_repositoryMock.Object, _repositoryMock2.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso()
        {
            var entidade = new TaskFlow("Teste");

            var command = new DeletarTaskFlowCommand(entidade.Id);

            _repositoryMock.Setup(r => r.DeleteAsync(entidade.Id))
                    .ReturnsAsync(true);

            _repositoryMock2.Setup(r => r.RemoveByFlow(entidade.Id))
                    .ReturnsAsync(true);

            var result = await _handler.Handle(command);

            Assert.True(result.IsSuccess);
            _repositoryMock.Verify(r => r.DeleteAsync(entidade.Id, CancellationToken.None), Times.Once);
        }
    }
}
