using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Exceptions;

namespace GateAPI.Domain.Validators
{
    public class MotoristaValidator : IValidator<Motorista>
    {
        public void Validar(Motorista e)
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(e.Nome))
                erros.Add("Nome é obrigatório");

            if (string.IsNullOrWhiteSpace(e.CNH.Value))
                erros.Add("CNH é obrigatório");

            if (string.IsNullOrWhiteSpace(e.RG.Value))
                erros.Add("RG é obrigatório");

            if (string.IsNullOrWhiteSpace(e.CPF.Value))
                erros.Add("CPF é obrigatório");

            if (erros.Any())
                throw new DomainRulesException(erros);
        }
    }
}

