using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Infra.Models.Configuracao;

namespace GateAPI.Tests.Entities.Configuracao
{
    public class PerfilTests
    {
        private readonly StatusEnum _validStatusEnum = StatusEnum.ATIVO;

        [Fact]
        public void Constructor_WithValidParameters_ShouldCreatePerfil()
        {
            // Arrange
            var nome = "Perfil";

            // Act
            var entity = new Perfil(
                nome, null
            );

            // Assert
            Assert.Equal(nome, entity.Nome);
            Assert.Equal(_validStatusEnum, entity.Status);
        }

        [Theory]
        [InlineData("")]
        public void Constructor_WithInvalidPerfil_ShouldThrowArgumentException(string nome)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Perfil(
                nome,  null
            ));
        }

        [Fact]
        public void UpdateEntity_WithValidNewPerfil_ShouldUpdatePerfil()
        {
            // Arrange
            var nome = "Perfil";

            var entity = new Perfil(
                nome, null
            );

            var descricao = "Perfil";

            // Act
            entity.UpdateEntity(nome, descricao, _validStatusEnum);

            // Assert
            Assert.Equal(nome, entity.Nome);
            Assert.Equal(descricao, entity.Descricao);
        }

        [Fact]
        public void Load_WithValidNewUsuario_ShouldLoadUsuario()
        {
            // Arrange
            var model = new PerfilModel()
            {
                Id = Guid.NewGuid(),
                Nome = "Teste",
                Status = _validStatusEnum
            };

            // Act
            var entidade = Perfil.Load(
                model.Id,
                model.Nome,
                null,
                model.Status);

            // Assert
            Assert.Equal(model.Id, entidade.Id);
            Assert.Equal(model.Nome, entidade.Nome);
            Assert.Equal(model.Status, entidade.Status);
        }
    }
}
