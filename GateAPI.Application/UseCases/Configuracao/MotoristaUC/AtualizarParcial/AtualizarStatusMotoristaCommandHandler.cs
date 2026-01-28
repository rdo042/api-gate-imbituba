using GateAPI.Application.Common.Models;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.MotoristaUC.AtualizarParcial
{
    internal class AtualizarStatusMotoristaCommandHandler : IRequestHandler<AtualizarStatusMotoristaCommand, Result<StatusEnum>>
    {
        public readonly IMotoristaRepository _repository;

        public AtualizarStatusMotoristaCommandHandler(IMotoristaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<StatusEnum>> Handle(AtualizarStatusMotoristaCommand request, CancellationToken cancellationToken)
        {

            var motorista = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (motorista is null)
            {
                return Result<StatusEnum>.Failure("Tipo de avaria não encontrado.");
            }

            motorista.UpdateIfNullEntity(status: request.Status);

            await _repository.UpdateAsync(motorista, cancellationToken);
            
            return Result<StatusEnum>.Success(request.Status);
        }
    }
}
