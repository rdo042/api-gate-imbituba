using FluentValidation;
using GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Atualizar;

namespace GateAPI.Domain.Validators.TipoAvariaValidators
{
    /// <summary>
    /// Validador FluentValidation para AtualizarTipoAvariaCommand
    /// </summary>
    public class AtualizarTipoAvariaCommandValidator : AbstractValidator<AtualizarTipoAvariaCommand>
    {
        public AtualizarTipoAvariaCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("ID do tipo de avaria é obrigatório");

            RuleFor(x => x.Tipo)
                .NotEmpty()
                .WithMessage("Tipo de avaria é obrigatório")
                .MinimumLength(3)
                .WithMessage("Tipo deve ter no mínimo 3 caracteres")
                .MaximumLength(50)
                .WithMessage("Tipo deve ter no máximo 50 caracteres");

            RuleFor(x => x.Descricao)
                .MaximumLength(300)
                .WithMessage("Descrição deve ter no máximo 300 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Descricao));
        }
    }
}
