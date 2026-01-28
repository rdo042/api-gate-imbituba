using FluentValidation;
using GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Atualizar;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Validators.TipoAvariaValidators;
using Xunit;

namespace GateAPI.Tests.Validators.Configuracao.TipoAvariaValidators
{
    public class AtualizarTipoAvariaCommandValidatorTests
    {
        private readonly AtualizarTipoAvariaCommandValidator _validator;
        private readonly Guid _validId = Guid.NewGuid();

        public AtualizarTipoAvariaCommandValidatorTests()
        {
            _validator = new AtualizarTipoAvariaCommandValidator();
        }

        #region ID Tests

        [Fact]
        public void Validate_DeveFalhar_QuandoIdEstaVazio()
        {
            // Arrange
            var command = new AtualizarTipoAvariaCommand(
                Guid.Empty,
                "Motor Queimado",
                "Descrição válida",
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Id" && e.ErrorMessage.Contains("obrigatório"));
        }

        [Fact]
        public void Validate_DeveRetornarSucesso_QuandoIdEValido()
        {
            // Arrange
            var command = new AtualizarTipoAvariaCommand(
                _validId,
                "Motor Queimado",
                "Descrição válida",
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }

        #endregion

        #region Tipo Tests

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Validate_DeveFalhar_QuandoTipoEstaVazio(string tipo)
        {
            // Arrange
            var command = new AtualizarTipoAvariaCommand(
                _validId,
                tipo,
                "Descrição válida",
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Tipo" && e.ErrorMessage.Contains("obrigatório"));
        }

        [Fact]
        public void Validate_DeveFalhar_QuandoTipoMenorQueMinimo()
        {
            // Arrange
            var command = new AtualizarTipoAvariaCommand(
                _validId,
                "AB",
                "Descrição válida",
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Tipo" && e.ErrorMessage.Contains("mínimo 3 caracteres"));
        }

        [Fact]
        public void Validate_DeveFalhar_QuandoTipoMaiorQueMaximo()
        {
            // Arrange
            var tipoLongo = new string('A', 51);
            var command = new AtualizarTipoAvariaCommand(
                _validId,
                tipoLongo,
                "Descrição válida",
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Tipo" && e.ErrorMessage.Contains("máximo 50 caracteres"));
        }

        #endregion

        #region Descricao Tests

        [Fact]
        public void Validate_DeveRetornarSucesso_QuandoDescricaoEhNula()
        {
            // Arrange
            var command = new AtualizarTipoAvariaCommand(
                _validId,
                "Motor Queimado",
                null,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_DeveRetornarSucesso_QuandoDescricaoEstaVazia()
        {
            // Arrange
            var command = new AtualizarTipoAvariaCommand(
                _validId,
                "Motor Queimado",
                "",
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_DeveFalhar_QuandoDescricaoMaiorQueMaximo()
        {
            // Arrange
            var descricaoLonga = new string('A', 301);
            var command = new AtualizarTipoAvariaCommand(
                _validId,
                "Motor Queimado",
                descricaoLonga,
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Descricao" && e.ErrorMessage.Contains("máximo 300 caracteres"));
        }

        [Fact]
        public void Validate_DeveRetornarSucesso_QuandoDescricaoEValida()
        {
            // Arrange
            var command = new AtualizarTipoAvariaCommand(
                _validId,
                "Motor Queimado",
                "Descrição com detalhes válida",
                StatusEnum.ATIVO
            );

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }

        #endregion
    }
}