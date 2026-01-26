using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;

namespace GateAPI.Tests.Entities.Configuracao
{
    public class IdiomaTests
    {
        private readonly StatusEnum _validStatusEnum = StatusEnum.ATIVO;
        private readonly int _validCanal = 1; // App

        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateIdioma()
        {
            // Arrange
            var codigo = "pt-BR";
            var nome = "Português Brasil";
            var descricao = "Idioma português do Brasil";

            // Act
            var entity = new Idioma(codigo, nome, descricao, _validStatusEnum, (Domain.Enums.CanalEnum)_validCanal, false);

            // Assert
            Assert.Equal(codigo, entity.Codigo);
            Assert.Equal(nome, entity.Nome);
            Assert.Equal(descricao, entity.Descricao);
            Assert.Equal(_validStatusEnum, entity.Status);
            Assert.False(entity.EhPadrao);
        }

        [Theory]
        [InlineData("")] // Código vazio
        [InlineData("   ")] // Código apenas com espaços
        public void Constructor_WithInvalidCodigo_ShouldThrowArgumentException(string codigo)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Idioma(
                codigo, "Português", "Descrição", _validStatusEnum, (Domain.Enums.CanalEnum)_validCanal, false
            ));
        }

        [Fact]
        public void Constructor_WithInvalidISO639_ShouldThrowArgumentException()
        {
            // Act & Assert - Código inválido (não segue ISO BCP 47)
            Assert.Throws<ArgumentException>(() => new Idioma(
                "INVALID123", "Português", "Descrição", _validStatusEnum, (Domain.Enums.CanalEnum)_validCanal, false
            ));
        }

        [Fact]
        public void Constructor_WithCodigoTooLong_ShouldThrowArgumentException()
        {
            // Arrange
            var codigoLongo = new string('a', 11);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Idioma(
                codigoLongo, "Português", "Descrição", _validStatusEnum, (Domain.Enums.CanalEnum)_validCanal, false
            ));
        }

        [Theory]
        [InlineData("")] // Nome vazio
        [InlineData("   ")] // Nome apenas com espaços
        public void Constructor_WithInvalidNome_ShouldThrowArgumentException(string nome)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Idioma(
                "pt-BR", nome, "Descrição", _validStatusEnum, (Domain.Enums.CanalEnum)_validCanal, false
            ));
        }

        [Fact]
        public void Constructor_WithNomeTooLong_ShouldThrowArgumentException()
        {
            // Arrange
            var nomeLongo = new string('A', 51);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Idioma(
                "pt-BR", nomeLongo, "Descrição", _validStatusEnum, (Domain.Enums.CanalEnum)_validCanal, false
            ));
        }

        [Fact]
        public void Constructor_WithDescricaoTooLong_ShouldThrowArgumentException()
        {
            // Arrange
            var descricaoLonga = new string('A', 256);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Idioma(
                "pt-BR", "Português", descricaoLonga, _validStatusEnum, (Domain.Enums.CanalEnum)_validCanal, false
            ));
        }

        [Fact]
        public void Constructor_WithPadraoAndInactiveStatus_ShouldThrowArgumentException()
        {
            // Act & Assert - Idioma padrão não pode estar INATIVO
            Assert.Throws<ArgumentException>(() => new Idioma(
                "pt-BR", "Português", "Descrição", StatusEnum.INATIVO, (Domain.Enums.CanalEnum)_validCanal, true
            ));
        }

        [Fact]
        public void DefinirComoPadrao_WithInactiveStatus_ShouldThrowArgumentException()
        {
            // Arrange
            var entity = new Idioma("pt-BR", "Português", "Descrição", StatusEnum.INATIVO, (Domain.Enums.CanalEnum)_validCanal, false);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => entity.DefinirComoPadrao());
        }

        [Fact]
        public void DefinirComoPadrao_WithActiveStatus_ShouldSetEhPadraoTrue()
        {
            // Arrange
            var entity = new Idioma("pt-BR", "Português", "Descrição", _validStatusEnum, (Domain.Enums.CanalEnum)_validCanal, false);

            // Act
            entity.DefinirComoPadrao();

            // Assert
            Assert.True(entity.EhPadrao);
        }

        [Fact]
        public void RemoverComoPadrao_ShouldSetEhPadraoFalse()
        {
            // Arrange
            var entity = new Idioma("pt-BR", "Português", "Descrição", _validStatusEnum, (Domain.Enums.CanalEnum)_validCanal, true);

            // Act
            entity.RemoverComoPadrao();

            // Assert
            Assert.False(entity.EhPadrao);
        }

        [Fact]
        public void AlterarStatus_WithPadraoAndInactiveStatus_ShouldThrowArgumentException()
        {
            // Arrange
            var entity = new Idioma("pt-BR", "Português", "Descrição", _validStatusEnum, (Domain.Enums.CanalEnum)_validCanal, true);

            // Act & Assert - Idioma padrão não pode ser inativado
            Assert.Throws<ArgumentException>(() => entity.AlterarStatus(StatusEnum.INATIVO));
        }

        [Fact]
        public void AlterarStatus_WithNonPadraoAndInactiveStatus_ShouldSucceed()
        {
            // Arrange
            var entity = new Idioma("pt-BR", "Português", "Descrição", _validStatusEnum, (Domain.Enums.CanalEnum)_validCanal, false);

            // Act
            entity.AlterarStatus(StatusEnum.INATIVO);

            // Assert
            Assert.Equal(StatusEnum.INATIVO, entity.Status);
        }

        [Fact]
        public void UpdateEntity_WithValidParameters_ShouldUpdateIdioma()
        {
            // Arrange
            var entity = new Idioma("pt-BR", "Português", "Descrição antiga", _validStatusEnum, (Domain.Enums.CanalEnum)_validCanal, false);

            var novoNome = "Português Brasileiro";
            var novaDescricao = "Nova descrição";

            // Act
            entity.UpdateEntity("en-US", novoNome, novaDescricao, _validStatusEnum, (Domain.Enums.CanalEnum)_validCanal);

            // Assert
            Assert.Equal("en-US", entity.Codigo);
            Assert.Equal(novoNome, entity.Nome);
            Assert.Equal(novaDescricao, entity.Descricao);
        }

        [Fact]
        public void Load_WithValidParameters_ShouldLoadIdioma()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var entidade = Idioma.Load(id, "pt-BR", "Português", "Descrição", _validStatusEnum, (Domain.Enums.CanalEnum)_validCanal, false);

            // Assert
            Assert.Equal(id, entidade.Id);
            Assert.Equal("pt-BR", entidade.Codigo);
            Assert.Equal("Português", entidade.Nome);
            Assert.False(entidade.EhPadrao);
        }

        [Fact]
        public void Constructor_ShouldInitializeBaseProperties()
        {
            // Arrange & Act
            var entity = new Idioma("pt-BR", "Português", "Descrição", _validStatusEnum, (Domain.Enums.CanalEnum)_validCanal, false);

            // Assert - Verificar que ID foi gerado
            Assert.NotEqual(Guid.Empty, entity.Id);
            Assert.NotNull(entity.Codigo);
            Assert.NotNull(entity.Nome);
        }

        [Fact]
        public void Load_ShouldSetAllProperties()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var entidade = Idioma.Load(
                id, 
                "pt-BR", 
                "Português", 
                "Descrição", 
                _validStatusEnum, 
                (Domain.Enums.CanalEnum)_validCanal, 
                false
            );

            // Assert
            Assert.Equal(id, entidade.Id);
            Assert.NotNull(entidade); // Entidade foi criada com sucesso
            Assert.Equal("pt-BR", entidade.Codigo);
            Assert.Equal("Português", entidade.Nome);
            Assert.Equal("Descrição", entidade.Descricao);
            Assert.Equal(_validStatusEnum, entidade.Status);
            Assert.False(entidade.EhPadrao);
        }
    }
}
