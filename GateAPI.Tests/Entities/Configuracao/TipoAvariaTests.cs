using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Infra.Models.Configuracao;

namespace GateAPI.Tests.Entities.Configuracao
{
    public class TipoAvariaTests
    {
        private readonly StatusEnum _validStatusEnum = StatusEnum.ATIVO;

        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateTipoAvaria()
        {
            // Arrange
            var tipo = "AVARIA001";
            var desc = "AVARIA001";

            // Act
            var entity = new TipoAvaria(
                tipo, desc, _validStatusEnum
            );

            // Assert
            Assert.Equal(tipo, entity.Tipo);
            Assert.Equal(desc, entity.Descricao);
            Assert.Equal(_validStatusEnum, entity.Status);
        }

        [Theory]
        [InlineData("")]
        public void Constructor_WithInvalidTipoAvaria_ShouldThrowArgumentException(string tipo)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new TipoAvaria(
                tipo, "AVARIA001", _validStatusEnum
            ));
        }

        [Fact]
        public void UpdateEntity_WithValidNewTipoAvaria_ShouldUpdateTipoAvaria()
        {
            // Arrange
            var entity = new TipoAvaria(
                "tipo", "desc", _validStatusEnum 
            );

            var newTipo = "AVARIA001";

            // Act
            entity.UpdateEntity(newTipo, null, _validStatusEnum);

            // Assert
            Assert.Equal(newTipo, entity.Tipo);
            Assert.Null(entity.Descricao);
        }

        [Fact]
        public void Load_WithValidNewTipoAvaria_ShouldLoadTipoAvaria()
        {
            // Arrange
            var model = new TipoAvariaModel()
            {
                Id = Guid.NewGuid(),
                Tipo = "LAC001",
                Descricao = "",
                Status = _validStatusEnum
            };

            // Act
            var entidade = TipoAvaria.Load(
                model.Id,
                model.Tipo,
                model.Descricao,
                model.Status);

            // Assert
            Assert.Equal(model.Id, entidade.Id);
            Assert.Equal(model.Tipo, entidade.Tipo);
            Assert.Equal(model.Descricao, entidade.Descricao);
            Assert.Equal(model.Status, entidade.Status);
        }
    }
}
