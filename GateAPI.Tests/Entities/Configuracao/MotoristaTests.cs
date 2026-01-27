using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Tests.Entities.Configuracao.Stubs;

namespace GateAPI.Tests.Entities.Configuracao
{
    public class MotoristaTests
    {
        private readonly StatusEnum _validStatusEnum = StatusEnum.ATIVO;

        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateMotorista()
        {
            // Arrange
            var nome = "João da Silva";
            var dataNascimento = new DateOnly(1985, 5, 20);
            var rg = "123456789";
            var cpf = "12345678901";
            var cnh = "12345678900";
            var validadeCnh = new DateOnly(2026, 12, 31);
            var telefone = "31999999999";
            var foto = "https://example.com/foto.jpg";

            // Act
            var entity = Motorista.Create(
                nome, dataNascimento, rg, cpf, cnh, validadeCnh, telefone, foto, _validStatusEnum
            );

            // Assert
            Assert.Equal(nome, entity.Nome);
            Assert.Equal(dataNascimento, entity.DataNascimento);
            Assert.Equal(rg, entity.RG.Value);
            Assert.Equal(cpf, entity.CPF.Value);
            Assert.Equal(cnh, entity.CNH.Value);
            Assert.Equal(validadeCnh, entity.ValidadeCNH);
            Assert.Equal(telefone, entity.Telefone);
            Assert.Equal(foto, entity.Foto);
            Assert.Equal(_validStatusEnum, entity.Status);
        }

        [Fact]
        public void Constructor_WithNullOptionalParameters_ShouldCreateMotorista()
        {
            // Arrange
            var nome = "Jane Silva";
            var rg = "987654321";
            var cpf = "98765432109";
            var cnh = "98765432101";

            // Act
            var entity = Motorista.Create(
                nome, null, rg, cpf, cnh, null, null, null, _validStatusEnum
            );

            // Assert
            Assert.Equal(nome, entity.Nome);
            Assert.Null(entity.DataNascimento);
            Assert.Equal(rg, entity.RG.Value);
            Assert.Equal(cpf, entity.CPF.Value);
            Assert.Equal(cnh, entity.CNH.Value);
            Assert.Null(entity.ValidadeCNH);
            Assert.Null(entity.Telefone);
            Assert.Null(entity.Foto);
            Assert.Equal(_validStatusEnum, entity.Status);
        }

        [Theory]
        [InlineData("")] // Nome vazio
        [InlineData("   ")] // Nome com apenas espaços
        public void Constructor_WithInvalidNome_ShouldThrowArgumentException(string nome)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => Motorista.Create(
                nome, new DateOnly(1985, 5, 20), "123456789", "12345678901", "12345678900", null, null, null, _validStatusEnum
            ));
        }

        [Fact]
        public void Constructor_WithInvalidCPF_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => Motorista.Create(
                "João da Silva", new DateOnly(1985, 5, 20), "123456789", "", "12345678900", null, null, null, _validStatusEnum
            ));
        }

        [Fact]
        public void Constructor_WithInvalidRG_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => Motorista.Create(
                "João da Silva", new DateOnly(1985, 5, 20), "", "12345678901", "12345678900", null, null, null, _validStatusEnum
            ));
        }

        [Fact]
        public void Constructor_WithInvalidCNH_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => Motorista.Create(
                "João da Silva", new DateOnly(1985, 5, 20), "123456789", "12345678901", "", null, null, null, _validStatusEnum
            ));
        }

        [Fact]
        public void UpdateEntity_WithValidNome_ShouldUpdateOnlyNome()
        {
            // Arrange
            var motorista = MotoristaStub.Valid();
            var nomeAnterior = motorista.Nome;
            var novoNome = "Maria Silva";

            // Act
            motorista.UpdateEntity(novoNome);

            // Assert
            Assert.NotEqual(nomeAnterior, motorista.Nome);
            Assert.Equal(novoNome, motorista.Nome);
        }

        [Fact]
        public void UpdateEntity_WithValidNomeAndDataNascimento_ShouldUpdateBoth()
        {
            // Arrange
            var motorista = MotoristaStub.Valid();
            var novoNome = "Maria Silva";
            var novaDataNascimento = new DateOnly(1995, 8, 10);

            // Act
            motorista.UpdateEntity(novoNome, novaDataNascimento);

            // Assert
            Assert.Equal(novoNome, motorista.Nome);
            Assert.Equal(novaDataNascimento, motorista.DataNascimento);
        }

        [Fact]
        public void UpdateEntity_WithAllParameters_ShouldUpdateAll()
        {
            // Arrange
            var motorista = MotoristaStub.Valid();
            var novoNome = "Maria Silva";
            var novaDataNascimento = new DateOnly(1990, 3, 15);
            var novoRG = "987654321";
            var novoCPF = "98765432109";
            var novoCNH = "98765432101";
            var novoTelefone = "31988888888";
            var novaFoto = "https://example.com/nova-foto.jpg";
            var novoStatus = StatusEnum.INATIVO;

            // Act
            motorista.UpdateEntity(novoNome, novaDataNascimento, novoRG, novoCPF, novoCNH, novoTelefone, novaFoto, novoStatus);

            // Assert
            Assert.Equal(novoNome, motorista.Nome);
            Assert.Equal(novaDataNascimento, motorista.DataNascimento);
            Assert.Equal(novoRG, motorista.RG.Value);
            Assert.Equal(novoCPF, motorista.CPF.Value);
            Assert.Equal(novoCNH, motorista.CNH.Value);
            Assert.Equal(novoTelefone, motorista.Telefone);
            Assert.Equal(novaFoto, motorista.Foto);
            Assert.Equal(novoStatus, motorista.Status);
        }

        [Fact]
        public void UpdateIfNullEntity_WithNomeOnly_ShouldUpdateOnlyNome()
        {
            // Arrange
            var motorista = MotoristaStub.Valid();
            var nomeAnterior = motorista.Nome;
            var novoNome = "Carlos Silva";

            // Act
            motorista.UpdateIfNullEntity(nome: novoNome);

            // Assert
            Assert.NotEqual(nomeAnterior, motorista.Nome);
            Assert.Equal(novoNome, motorista.Nome);
        }

        [Fact]
        public void UpdateIfNullEntity_WithMultipleParameters_ShouldUpdateOnlyProvidedValues()
        {
            // Arrange
            var motorista = MotoristaStub.Valid();
            var nomeAnterior = motorista.Nome;
            var rgAnterior = motorista.RG.Value;
            var novoNome = "Patricia Silva";
            var novaDataNascimento = new DateOnly(1992, 12, 25);

            // Act
            motorista.UpdateIfNullEntity(nome: novoNome, dataNascimento: novaDataNascimento);

            // Assert
            Assert.NotEqual(nomeAnterior, motorista.Nome);
            Assert.Equal(novoNome, motorista.Nome);
            Assert.Equal(novaDataNascimento, motorista.DataNascimento);
            Assert.Equal(rgAnterior, motorista.RG.Value); // RG não deve mudar
        }

        [Fact]
        public void UpdateIfNullEntity_WithEmptyStringValues_ShouldNotUpdate()
        {
            // Arrange
            var motorista = MotoristaStub.Valid();
            var nomeAnterior = motorista.Nome;
            var cpfAnterior = motorista.CPF.Value;

            // Act
            motorista.UpdateIfNullEntity(nome: "", cpf: "");

            // Assert
            Assert.Equal(nomeAnterior, motorista.Nome); // Não deve atualizar
            Assert.Equal(cpfAnterior, motorista.CPF.Value); // Não deve atualizar
        }

        [Fact]
        public void UpdateIfNullEntity_WithWhitespaceValues_ShouldNotUpdate()
        {
            // Arrange
            var motorista = MotoristaStub.Valid();
            var telefoneAnterior = motorista.Telefone;

            // Act
            motorista.UpdateIfNullEntity(telefone: "   ");

            // Assert
            Assert.Equal(telefoneAnterior, motorista.Telefone); // Não deve atualizar
        }
    }
}
