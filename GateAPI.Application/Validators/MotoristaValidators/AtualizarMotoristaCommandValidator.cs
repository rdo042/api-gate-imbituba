using FluentValidation;
using GateAPI.Application.UseCases.Configuracao.MotoristaUC.Atualizar;

namespace GateAPI.Domain.Validators.MotoristaValidators
{
    /// <summary>
    /// Validador FluentValidation para AtualizarMotoristaCommand
    /// </summary>
    public class AtualizarMotoristaCommandValidator : AbstractValidator<AtualizarMotoristaCommand>
    {
        public AtualizarMotoristaCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("ID do motorista é obrigatório");

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome é obrigatório")
                .MinimumLength(3)
                .WithMessage("Nome deve ter no mínimo 3 caracteres")
                .MaximumLength(100)
                .WithMessage("Nome deve ter no máximo 100 caracteres");

            RuleFor(x => x.CPF)
                .NotEmpty()
                .WithMessage("CPF é obrigatório")
                .Length(11)
                .WithMessage("CPF deve ter exatamente 11 caracteres");

            RuleFor(x => x.RG)
                .NotEmpty()
                .WithMessage("RG é obrigatório")
                .MaximumLength(20)
                .WithMessage("RG deve ter no máximo 20 caracteres");

            RuleFor(x => x.CNH)
                .NotEmpty()
                .WithMessage("CNH é obrigatória")
                .Length(11)
                .WithMessage("CNH deve ter exatamente 11 caracteres");

            RuleFor(x => x.Telefone)
                .MaximumLength(20)
                .WithMessage("Telefone deve ter no máximo 20 caracteres")
                .Matches(@"^\d{10,11}$")
                .WithMessage("Telefone deve conter entre 10 e 11 dígitos")
                .When(x => !string.IsNullOrEmpty(x.Telefone));

            RuleFor(x => x.Foto)
                .MaximumLength(500)
                .WithMessage("URL da foto deve ter no máximo 500 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Foto));

            RuleFor(x => x.ValidadeCnh)
                .GreaterThan(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Data de validade da CNH não pode ser no passado")
                .When(x => x.ValidadeCnh.HasValue);
        }
    }
}
