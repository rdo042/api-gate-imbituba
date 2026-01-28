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
            Assert.NotEqual(Guid.Empty, entity.Id);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_WithInvalidLocal_ShouldThrowArgumentException(string local)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new LocalAvaria(
                local, "Descrição válida", _validStatusEnum
            ));
        }

        [Fact]
        public void Constructor_WithNullLocal_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new LocalAvaria(
                null, "Descrição válida", _validStatusEnum
            ));
        }

        [Fact]
        public void Constructor_WithLocalTooLong_ShouldThrowArgumentException()
        {
            // Arrange
            var localLongo = new string('A', 101);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new LocalAvaria(
                localLongo, "Descrição válida", _validStatusEnum
            ));
        }

        [Fact]
        public void Constructor_WithMaxLengthLocal_ShouldSucceed()
        {
            // Arrange
            var localMaximo = new string('A', 100);
            var descricao = "Descrição válida";

            // Act
            var entity = new LocalAvaria(localMaximo, descricao, _validStatusEnum);

            // Assert
            Assert.Equal(localMaximo, entity.Local);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_WithInvalidDescricao_ShouldThrowArgumentException(string descricao)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new LocalAvaria(
                "LOCAL001", descricao, _validStatusEnum
            ));
        }

        [Fact]
        public void Constructor_WithNullDescricao_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new LocalAvaria(
                "LOCAL001", null, _validStatusEnum
            ));
        }

        [Fact]
        public void Constructor_WithDescricaoTooLong_ShouldThrowArgumentException()
        {
            // Arrange
            var descricaoLonga = new string('A', 256);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new LocalAvaria(
                "LOCAL001", descricaoLonga, _validStatusEnum
            ));
        }

        [Fact]
        public void Constructor_WithMaxLengthDescricao_ShouldSucceed()
        {
            // Arrange
            var local = "LOCAL001";
            var descricaoMaxima = new string('A', 255);

            // Act
            var entity = new LocalAvaria(local, descricaoMaxima, _validStatusEnum);

            // Assert
            Assert.Equal(descricaoMaxima, entity.Descricao);
        }

        [Fact]
        public void Constructor_WithInactiveStatus_ShouldSucceed()
        {
            // Arrange & Act
            var entity = new LocalAvaria("LOCAL001", "Descrição", StatusEnum.INATIVO);

            // Assert
            Assert.Equal(StatusEnum.INATIVO, entity.Status);
        }

        [Fact]
        public void Constructor_ShouldGenerateNewGuid_AsId()
        {
            // Arrange
            var localAvaria1 = new LocalAvaria("LOCAL001", "Descrição 1", _validStatusEnum);
            var localAvaria2 = new LocalAvaria("LOCAL002", "Descrição 2", _validStatusEnum);

            // Act & Assert
            Assert.NotEqual(localAvaria1.Id, localAvaria2.Id);
            Assert.NotEqual(Guid.Empty, localAvaria1.Id);
            Assert.NotEqual(Guid.Empty, localAvaria2.Id);
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
        public void UpdateEntity_WithEmptyLocal_ShouldThrowArgumentException()
        {
            // Arrange
            var entity = new LocalAvaria("LOCAL001", "Descrição", _validStatusEnum);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => entity.UpdateEntity("", "Nova descrição", _validStatusEnum));
        }

        [Fact]
        public void UpdateEntity_WithLocalTooLong_ShouldThrowArgumentException()
        {
            // Arrange
            var entity = new LocalAvaria("LOCAL001", "Descrição", _validStatusEnum);
            var localLongo = new string('A', 101);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => entity.UpdateEntity(localLongo, "Nova descrição", _validStatusEnum));
        }

        [Fact]
        public void UpdateEntity_WithEmptyDescricao_ShouldThrowArgumentException()
        {
            // Arrange
            var entity = new LocalAvaria("LOCAL001", "Descrição", _validStatusEnum);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => entity.UpdateEntity("LOCAL002", "", _validStatusEnum));
        }

        [Fact]
        public void UpdateEntity_WithDescricaoTooLong_ShouldThrowArgumentException()
        {
            // Arrange
            var entity = new LocalAvaria("LOCAL001", "Descrição", _validStatusEnum);
            var descricaoLonga = new string('A', 256);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => entity.UpdateEntity("LOCAL002", descricaoLonga, _validStatusEnum));
        }

        [Fact]
        public void UpdateEntity_CanChangeStatus()
        {
            // Arrange
            var entity = new LocalAvaria("LOCAL001", "Descrição", StatusEnum.ATIVO);

            // Act
            entity.UpdateEntity("LOCAL001", "Descrição", StatusEnum.INATIVO);

            // Assert
            Assert.Equal(StatusEnum.INATIVO, entity.Status);
        }

        [Fact]
        public void Load_WithValidParameters_ShouldLoadLocalAvaria()
        {
            // Arrange
            var id = Guid.NewGuid();
            var local = "LOCAL001";
            var descricao = "Descrição do local";

            // Act
            var entidade = LocalAvaria.Load(
                id,
                local,
                descricao,
                _validStatusEnum
            );

            // Assert
            Assert.Equal(id, entidade.Id);
            Assert.Equal(local, entidade.Local);
            Assert.Equal(descricao, entidade.Descricao);
            Assert.Equal(_validStatusEnum, entidade.Status);
        }

        [Fact]
        public void Load_WithEmptyLocal_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => LocalAvaria.Load(
                Guid.NewGuid(),
                "",
                "Descrição",
                _validStatusEnum
            ));
        }

        [Fact]
        public void Load_WithEmptyDescricao_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => LocalAvaria.Load(
                Guid.NewGuid(),
                "LOCAL001",
                "",
                _validStatusEnum
            ));
        }

        [Fact]
        public void Load_WithLocalTooLong_ShouldThrowArgumentException()
        {
            // Arrange
            var localLongo = new string('A', 101);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => LocalAvaria.Load(
                Guid.NewGuid(),
                localLongo,
                "Descrição",
                _validStatusEnum
            ));
        }

        [Fact]
        public void Load_WithDescricaoTooLong_ShouldThrowArgumentException()
        {
            // Arrange
            var descricaoLonga = new string('A', 256);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => LocalAvaria.Load(
                Guid.NewGuid(),
                "LOCAL001",
                descricaoLonga,
                _validStatusEnum
            ));
        }

        [Fact]
        public void Constructor_WithMultilineDescricao_ShouldSucceed()
        {
            // Arrange
            var local = "LOCAL001";
            var descricaoMultilinha = "Primeira linha\nSegunda linha\nTerceira linha";

            // Act
            var entity = new LocalAvaria(local, descricaoMultilinha, _validStatusEnum);

            // Assert
            Assert.Equal(descricaoMultilinha, entity.Descricao);
        }

        [Fact]
        public void Constructor_WithSpecialCharacters_ShouldSucceed()
        {
            // Arrange
            var local = "LOCAL_001@#";
            var descricaoComEspeciais = "Descrição com caracteres especiais: @#$%&*()";

            // Act
            var entity = new LocalAvaria(local, descricaoComEspeciais, _validStatusEnum);

            // Assert
            Assert.Equal(local, entity.Local);
            Assert.Equal(descricaoComEspeciais, entity.Descricao);
        }

        [Fact]
        public void UpdateEntity_ShouldValidateAfterUpdate()
        {
            // Arrange
            var entity = new LocalAvaria("LOCAL001", "Descrição original", _validStatusEnum);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => entity.UpdateEntity("", "Nova descrição", _validStatusEnum));
        }
    }
}

