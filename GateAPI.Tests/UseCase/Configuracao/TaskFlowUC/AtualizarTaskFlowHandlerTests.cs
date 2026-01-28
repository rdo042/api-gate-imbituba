using GateAPI.Application.UseCases.Configuracao.TaskFlowUC.Atualizar;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.TaskFlowUC
{
    public class AtualizarTaskFlowHandlerTests
    {
        private readonly Mock<ITaskFlowRepository> _repositoryMock;
        private readonly AtualizarTaskFlowHandler _handler;
        public AtualizarTaskFlowHandlerTests()
        {
            _repositoryMock = new Mock<ITaskFlowRepository>();
            _handler = new AtualizarTaskFlowHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucesso()
        {
            var entidade = TaskFlow.Load(Guid.NewGuid(), "Teste", []);

            var command = new AtualizarTaskFlowCommand(entidade.Id, entidade.Nome);

            _repositoryMock.Setup(r => r.GetByIdAsync(entidade.Id))
                           .ReturnsAsync(entidade);

            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<TaskFlow>()))
                           .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command);

            Assert.True(result.IsSuccess);
            _repositoryMock.Verify(r => r.UpdateAsync(It.Is<TaskFlow>(n =>
                n.Nome == "Teste"
            )), Times.Once);
        }
    }
}
