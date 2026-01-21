using GateAPI.Application.UseCases.Configuracao.UsuarioUC.BuscarPorId;
using GateAPI.Application.UseCases.Configuracao.UsuarioUC.BuscarTodosPorParametro;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using Moq;

namespace GateAPI.Tests.UseCase.Configuracao.UsuarioUC
{
    public class BuscarTodosPorParametroUsuarioHandlerTests
    {
        private readonly Mock<IUsuarioRepository> _repositoryMock;
        private readonly BuscarTodosPorParametroUsuarioHandler _handler;
        public BuscarTodosPorParametroUsuarioHandlerTests()
        {
            _repositoryMock = new Mock<IUsuarioRepository>();
            _handler = new BuscarTodosPorParametroUsuarioHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleAsync_DeveRetornarSucesso()
        {
            // Arrange
            var nome = "John Doe";
            var email = "johndoe@email.com";
            var senha = "senha_hash";

            IEnumerable<Usuario> existente = [ new (
                nome, email, senha, null, null
            )];

            var command = new BuscarTodosPorParametroUsuarioQuery(1, 1, null, "asc", "john");

            _repositoryMock.Setup(r => r.GetAllPaginatedAsync(command.Page, command.PageSize, command.SortColumn, command.SortDirection, command.Nome))
                    .ReturnsAsync((existente, 1));
                
            // Act: Executamos o Handler
            var result = await _handler.HandleAsync(command);

            Assert.True(result.IsSuccess);
            _repositoryMock.Verify(r => r.GetAllPaginatedAsync(command.Page, command.PageSize, command.SortColumn, command.SortDirection, command.Nome), Times.Once);
        }
    }
}
