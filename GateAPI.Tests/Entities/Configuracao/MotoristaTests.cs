using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Exceptions;
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
            var stub = MotoristaStub.Valid();

            // Act
            var entity = Motorista.Create(
                stub.Nome, stub.DataNascimento, stub.RG.Value, stub.CPF.Value, stub.CNH.Value, stub.ValidadeCNH, stub.Telefone, stub.Foto, _validStatusEnum
            );

            // Assert
            Assert.Equal(stub.Nome, entity.Nome);
            Assert.Equal(stub.DataNascimento, entity.DataNascimento);
            Assert.Equal(stub.RG.Value, entity.RG.Value);
            Assert.Equal(stub.CPF.Value, entity.CPF.Value);
            Assert.Equal(stub.CNH.Value, entity.CNH.Value);
            Assert.Equal(stub.ValidadeCNH, entity.ValidadeCNH);
            Assert.Equal(stub.Telefone, entity.Telefone);
            Assert.Equal(stub.Foto, entity.Foto);
            Assert.Equal(_validStatusEnum, entity.Status);
        }

        [Fact]
        public void Constructor_WithNullOptionalParameters_ShouldCreateMotorista()
        {
            // Arrange
var stub = MotoristaStub.Valid();

            // Act
            var entity = Motorista.Create(
                stub.Nome, null, stub.RG.Value, stub.CPF.Value,stub.CNH.Value, null, null, null, _validStatusEnum
            );

            // Assert
            Assert.Equal(stub.Nome, entity.Nome);
            Assert.Null(entity.DataNascimento);
            Assert.Equal(stub.RG.Value, entity.RG.Value);
            Assert.Equal(stub.CPF.Value, entity.CPF.Value);
            Assert.Equal(stub.CNH.Value, entity.CNH.Value);
            Assert.Null(entity.ValidadeCNH);
            Assert.Null(entity.Telefone);
            Assert.Null(entity.Foto);
            Assert.Equal(_validStatusEnum, entity.Status);
        }

        [Theory]
        [InlineData("")] // Nome vazio
        [InlineData("   ")] // Nome com apenas espaços
        public void Constructor_WithInvalidNome_ShouldThrowDomainRulesException(string nome)
        {
            // Act & Assert
            Assert.Throws<DomainRulesException>(() => Motorista.Create(
                nome, new DateOnly(1985, 5, 20), "123456789", "12345678901", "12345678900", null, null, null, _validStatusEnum
            ));
        }

        [Fact]
        public void Constructor_WithInvalidCPF_ShouldThrowDomainRulesException()
        {
            // Act & Assert
            Assert.Throws<DomainRulesException>(() => Motorista.Create(
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
            var novoRG = "225645154";
            var novoCPF = "23943634019";
            var novoCNH = "58664847016";
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
