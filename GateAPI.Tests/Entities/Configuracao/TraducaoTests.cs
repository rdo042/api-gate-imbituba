using GateAPI.Domain.Entities.Configuracao;

namespace GateAPI.Tests.Entities.Configuracao
{
    public class TraducaoTests
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateTraducao()
        {
            // Arrange
            var idIdioma = Guid.NewGuid();
            var chave = "MENSAGEM_BOAS_VINDAS";
            var frase = "Bem-vindo ao sistema!";

            // Act
            var entity = new Traducao(idIdioma, chave, frase);

            // Assert
            Assert.NotEqual(Guid.Empty, entity.Id);
            Assert.Equal(idIdioma, entity.IdIdioma);
            Assert.Equal(chave, entity.Chave);
            Assert.Equal(frase, entity.Frase);
        }

        [Fact]
        public void Constructor_WithEmptyIdIdioma_ShouldThrowArgumentException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => new Traducao(
                Guid.Empty,
                "CHAVE_TESTE",
                "Frase de teste"
            ));
        }

        [Fact]
        public void Constructor_WithEmptyChave_ShouldThrowArgumentException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => new Traducao(
                Guid.NewGuid(),
                "",
                "Frase de teste"
            ));
        }

        [Theory]
        [InlineData("   ")] // Apenas espaços
        [InlineData(null)] // Nulo
        public void Constructor_WithInvalidChave_ShouldThrowArgumentException(string? chave)
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => new Traducao(
                Guid.NewGuid(),
                chave,
                "Frase de teste"
            ));
        }

        [Fact]
        public void Constructor_WithChaveTooLong_ShouldThrowArgumentException()
        {
            // Arrange
            var chaveLonga = new string('A', 101);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Traducao(
                Guid.NewGuid(),
                chaveLonga,
                "Frase de teste"
            ));
        }

        [Fact]
        public void Constructor_WithMaxLengthChave_ShouldSucceed()
        {
            // Arrange
            var idIdioma = Guid.NewGuid();
            var chaveMaxima = new string('A', 100);
            var frase = "Frase de teste";

            // Act
            var entity = new Traducao(idIdioma, chaveMaxima, frase);

            // Assert
            Assert.Equal(chaveMaxima, entity.Chave);
        }

        [Fact]
        public void Constructor_WithEmptyFrase_ShouldThrowArgumentException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => new Traducao(
                Guid.NewGuid(),
                "CHAVE_TESTE",
                ""
            ));
        }

        [Theory]
        [InlineData("   ")] // Apenas espaços
        [InlineData(null)] // Nulo
        public void Constructor_WithInvalidFrase_ShouldThrowArgumentException(string frase)
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => new Traducao(
                Guid.NewGuid(),
                "CHAVE_TESTE",
                frase
            ));
        }

        [Fact]
        public void Constructor_WithLongFrase_ShouldSucceed()
        {
            // Arrange
            var idIdioma = Guid.NewGuid();
            var chave = "MENSAGEM_LONGA";
            var fraseLonga = new string('A', 1000);

            // Act
            var entity = new Traducao(idIdioma, chave, fraseLonga);

            // Assert
            Assert.Equal(fraseLonga, entity.Frase);
        }

        [Fact]
        public void UpdateEntity_WithValidParameters_ShouldUpdateTraducao()
        {
            // Arrange
            var entity = new Traducao(
                Guid.NewGuid(),
                "CHAVE_ORIGINAL",
                "Frase original"
            );

            var novaChave = "CHAVE_ATUALIZADA";
            var novaFrase = "Frase atualizada com sucesso";

            // Act
            entity.UpdateEntity(novaChave, novaFrase);

            // Assert
            Assert.Equal(novaChave, entity.Chave);
            Assert.Equal(novaFrase, entity.Frase);
        }

        [Fact]
        public void UpdateEntity_WithEmptyChave_ShouldThrowArgumentException()
        {
            // Arrange
            var entity = new Traducao(
                Guid.NewGuid(),
                "CHAVE_ORIGINAL",
                "Frase original"
            );

            // Act & Assert
            Assert.Throws<ArgumentException>(() => entity.UpdateEntity("", "Nova frase"));
        }

        [Fact]
        public void UpdateEntity_WithChaveTooLong_ShouldThrowArgumentException()
        {
            // Arrange
            var entity = new Traducao(
                Guid.NewGuid(),
                "CHAVE_ORIGINAL",
                "Frase original"
            );

            var chaveLonga = new string('A', 101);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => entity.UpdateEntity(chaveLonga, "Nova frase"));
        }

        [Fact]
        public void UpdateEntity_WithEmptyFrase_ShouldThrowArgumentException()
        {
            // Arrange
            var entity = new Traducao(
                Guid.NewGuid(),
                "CHAVE_ORIGINAL",
                "Frase original"
            );

            // Act & Assert
            Assert.Throws<ArgumentException>(() => entity.UpdateEntity("NOVA_CHAVE", ""));
        }

        [Fact]
        public void Load_WithValidParameters_ShouldLoadTraducao()
        {
            // Arrange
            var id = Guid.NewGuid();
            var idIdioma = Guid.NewGuid();
            var chave = "CHAVE_CARREGADA";
            var frase = "Frase carregada do banco de dados";

            // Act
            var entity = Traducao.Load(id, idIdioma, chave, frase);

            // Assert
            Assert.Equal(id, entity.Id);
            Assert.Equal(idIdioma, entity.IdIdioma);
            Assert.Equal(chave, entity.Chave);
            Assert.Equal(frase, entity.Frase);
        }

        [Fact]
        public void Load_WithEmptyIdIdioma_ShouldThrowArgumentException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => Traducao.Load(
                Guid.NewGuid(),
                Guid.Empty,
                "CHAVE_TESTE",
                "Frase de teste"
            ));
        }

        [Fact]
        public void Load_WithInvalidChave_ShouldThrowArgumentException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => Traducao.Load(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "",
                "Frase de teste"
            ));
        }

        [Fact]
        public void Load_WithInvalidFrase_ShouldThrowArgumentException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => Traducao.Load(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "CHAVE_TESTE",
                ""
            ));
        }

        [Fact]
        public void Constructor_ShouldGenerateNewGuid_AsId()
        {
            // Arrange
            var traducao1 = new Traducao(Guid.NewGuid(), "CHAVE1", "Frase 1");
            var traducao2 = new Traducao(Guid.NewGuid(), "CHAVE2", "Frase 2");

            // Act & Assert
            Assert.NotEqual(traducao1.Id, traducao2.Id);
            Assert.NotEqual(Guid.Empty, traducao1.Id);
            Assert.NotEqual(Guid.Empty, traducao2.Id);
        }

        [Fact]
        public void UpdateEntity_ShouldValidateAfterUpdate()
        {
            // Arrange
            var entity = new Traducao(
                Guid.NewGuid(),
                "CHAVE_ORIGINAL",
                "Frase original"
            );

            // Act & Assert
            Assert.Throws<ArgumentException>(() => entity.UpdateEntity("", "Nova frase"));
        }

        [Fact]
        public void Constructor_WithMultilineText_ShouldSucceed()
        {
            // Arrange
            var idIdioma = Guid.NewGuid();
            var chave = "MENSAGEM_MULTILINHA";
            var fraseMultilinha = "Primeira linha\nSegunda linha\nTerceira linha";

            // Act
            var entity = new Traducao(idIdioma, chave, fraseMultilinha);

            // Assert
            Assert.Equal(fraseMultilinha, entity.Frase);
        }

        [Fact]
        public void Constructor_WithSpecialCharacters_ShouldSucceed()
        {
            // Arrange
            var idIdioma = Guid.NewGuid();
            var chave = "CHAVE_COM_NUMEROS_123";
            var fraseComEspeciais = "Olá! Como vai? @#$%&*()";

            // Act
            var entity = new Traducao(idIdioma, chave, fraseComEspeciais);

            // Assert
            Assert.Equal(chave, entity.Chave);
            Assert.Equal(fraseComEspeciais, entity.Frase);
        }
    }
}
