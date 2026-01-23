using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Infra.Models.Configuracao;

namespace GateAPI.Tests.Entities.Configuracao
{
    public class LocalAvariaTests
    {
        private readonly StatusEnum _validStatusEnum = StatusEnum.ATIVO;

        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateLocalAvaria()
        {
            // Arrange
            var local = "LOCAL001";
            var descricao = "Descrição do local de avaria";

            // Act
            var entity = new LocalAvaria(
                local, descricao, _validStatusEnum
            );

            // Assert
            Assert.Equal(local, entity.Local);
            Assert.Equal(descricao, entity.Descricao);
            Assert.Equal(_validStatusEnum, entity.Status);
        }

        [Theory]
        [InlineData("")]
        public void Constructor_WithInvalidLocal_ShouldThrowArgumentException(string local)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new LocalAvaria(
                local, "Descrição válida", _validStatusEnum
            ));
        }

        [Theory]
        [InlineData("")]
        public void Constructor_WithInvalidDescricao_ShouldThrowArgumentException(string descricao)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new LocalAvaria(
                "LOCAL001", descricao, _validStatusEnum
            ));
        }

        [Fact]
        public void UpdateEntity_WithValidParameters_ShouldUpdateLocalAvaria()
        {
            // Arrange
            var entity = new LocalAvaria(
                "LOCAL001", "Descrição inicial", _validStatusEnum 
            );

            var newLocal = "LOCAL002";
            var newDescricao = "Nova descrição";

            // Act
            entity.UpdateEntity(newLocal, newDescricao, _validStatusEnum);

            // Assert
            Assert.Equal(newLocal, entity.Local);
            Assert.Equal(newDescricao, entity.Descricao);
        }

        [Fact]
        public void Load_WithValidParameters_ShouldLoadLocalAvaria()
        {
            // Arrange
            var model = new LocalAvariaModel()
            {
                Id = Guid.NewGuid(),
                Local = "LOCAL001",
                Descricao = "Descrição do local",
                Status = _validStatusEnum
            };

            // Act
            var entidade = LocalAvaria.Load(
                model.Id,
                model.Local,
                model.Descricao,
                model.Status);

            // Assert
            Assert.Equal(model.Id, entidade.Id);
            Assert.Equal(model.Local, entidade.Local);
            Assert.Equal(model.Descricao, entidade.Descricao);
            Assert.Equal(model.Status, entidade.Status);
        }
    }
}
