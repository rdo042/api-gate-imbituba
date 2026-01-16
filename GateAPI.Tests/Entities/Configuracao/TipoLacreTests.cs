using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Infra.Models;

namespace GateAPI.Tests.Entities.Configuracao
{
    public class TipoLacreTests
    {
        private readonly StatusEnum _validStatusEnum = StatusEnum.ATIVO;

        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateTipoLacre()
        {
            // Arrange
            var tipo = "LACRE001";
            var desc = "LACRE001";

            // Act
            var entity = new TipoLacre(
                tipo, desc, _validStatusEnum
            );

            // Assert
            Assert.Equal(tipo, entity.Tipo);
            Assert.Equal(desc, entity.Descricao);
            Assert.Equal(_validStatusEnum, entity.Status);
        }

        [Theory]
        [InlineData("")]
        public void Constructor_WithInvalidTipoLacre_ShouldThrowArgumentException(string tipo)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new TipoLacre(
                tipo, "LACRE001", _validStatusEnum
            ));
        }

        [Fact]
        public void UpdateEntity_WithValidNewTipoLacre_ShouldUpdateTipoLacre()
        {
            // Arrange
            var entity = new TipoLacre(
                "tipo", "desc", _validStatusEnum 
            );

            var newTipo = "LACRE001";

            // Act
            entity.UpdateEntity(newTipo, null, _validStatusEnum);

            // Assert
            Assert.Equal(newTipo, entity.Tipo);
            Assert.Null(entity.Descricao);
        }

        [Fact]
        public void Load_WithValidNewTipoLacre_ShouldLoadTipoLacre()
        {
            // Arrange
            var model = new TipoLacreModel()
            {
                Id = Guid.NewGuid(),
                Tipo = "LAC001",
                Descricao = "",
                Status = _validStatusEnum
            };

            // Act
            var entidade = TipoLacre.Load(
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
