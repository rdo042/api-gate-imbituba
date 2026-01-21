using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Infra.Models.Configuracao;
using GateAPI.Infra.Services;

namespace GateAPI.Tests.Entities.Configuracao
{
    public class UsuarioTests()
    {
        private readonly StatusEnum _validStatusEnum = StatusEnum.ATIVO;
        private readonly Perfil? _perfil = null;
        private readonly PasswordHasher _hasher = new();

        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateUsuario()
        {
            // Arrange
            var nome = "John Doe";
            var email = "johndoe@email.com";
            var senha = "#Senha123";
            var senhaHash = _hasher.HashPassword(senha);

            // Act
            var entity = new Usuario(
                nome, email, senhaHash, "link", _perfil
            );

            // Assert
            Assert.Equal(nome, entity.Nome);
            Assert.Equal(email, entity.Email);
            Assert.NotEqual(senha, entity.SenhaHash);
            Assert.Equal(senhaHash, entity.SenhaHash);
            Assert.Equal(_validStatusEnum, entity.Status);
            Assert.Equal("link", entity.LinkFoto);
        }

        [Theory]
        [InlineData("")]
        public void Constructor_WithInvalidUsuario_ShouldThrowArgumentException(string nome)
        {
            // Act & Assert
            var senhaHash = _hasher.HashPassword("#Senha123");
            Assert.Throws<ArgumentException>(() => new Usuario(
                nome, "email", senhaHash, null, null
            ));
        }

        [Fact]
        public void UpdateEntity_WithValidNewUsuario_ShouldUpdateUsuario()
        {
            // Arrange
            var nome = "John Doe";
            var email = "johndoe@email.com";
            var senha = "#Senha123";
            var senhaHash = _hasher.HashPassword(senha);
            var linkFoto = "link";

            var entity = new Usuario(
                nome, email, senhaHash, linkFoto, _perfil
            );

            var newNome = "Jane Doe";

            // Act
            entity.UpdateEntity(newNome, email, senhaHash, linkFoto, _perfil, _validStatusEnum);

            // Assert
            Assert.Equal(newNome, entity.Nome);
            Assert.NotEqual(nome, entity.Nome);
        }

        [Fact]
        public void Load_WithValidNewUsuario_ShouldLoadUsuario()
        {
            // Arrange
            var model = new UsuarioModel()
            {
                Id = Guid.NewGuid(),
                Nome = "Teste",
                Email = "teste@email.com",
                SenhaHash = _hasher.HashPassword("Senha123"),
                Status = _validStatusEnum
            };

            // Act
            var entidade = Usuario.Load(
                model.Id,
                model.Nome,
                model.Email,
                model.SenhaHash,
                false,
                null,
                null,
                model.Status);

            // Assert
            Assert.Equal(model.Id, entidade.Id);
            Assert.Equal(model.Nome, entidade.Nome);
            Assert.Equal(model.Email, entidade.Email);
            Assert.Equal(model.Status, entidade.Status);
        }

        [Fact]
        public void UpdateStatus_WithValidNewStatus_ShouldChangeStatus()
        {
            // Arrange
            var entidade = new Usuario(
                "John Doe", "johndoe@email.com", "senha_hash", null, null
            );

            // Act
            entidade.UpdateStatus(StatusEnum.INATIVO);

            // Assert
            Assert.Equal(StatusEnum.INATIVO, entidade.Status);
        }
    }
}
