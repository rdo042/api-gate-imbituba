using GateAPI.Application.Common.Models;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Criar
{
    public class AtualizarMotoristaCommandHandler : IRequestHandler<AtualizarMotoristaCommand, Result<Guid>>
    {
        public readonly IMotoristaRepository _repository;

        public AtualizarMotoristaCommandHandler(IMotoristaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Guid>> Handle(AtualizarMotoristaCommand request, CancellationToken cancellationToken)
        {
            
            var motorista = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (motorista is null)
            {
                return Result<Guid>.Failure("Tipo de motorista não encontrado.");
            }

            motorista.UpdateEntity(request.Nome, request.DataNascimento, request.RG, request.CPF, request.CNH, request.Telefone, request.Foto, request.Status);

            await _repository.UpdateAsync(motorista, cancellationToken);
            return Result<Guid>.Success(request.Id);
        }
    }
}