using GateAPI.Application.Common.Models;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Atualizar
{
    public class AtualizarTipoAvariaCommandHandler : IRequestHandler<AtualizarTipoAvariaCommand, Result<Guid>>
    {
        public readonly ITipoAvariaRepository _tipoAvariaRepository;

        public AtualizarTipoAvariaCommandHandler(ITipoAvariaRepository tipoAvariaRepository)
        {
            _tipoAvariaRepository = tipoAvariaRepository;
        }

        public async Task<Result<Guid>> Handle(AtualizarTipoAvariaCommand request, CancellationToken cancellationToken)
        {
            
            var tipoAvaria = await _tipoAvariaRepository.GetByIdAsync(request.Id, cancellationToken);
            if (tipoAvaria is null)
            {
                return Result<Guid>.Failure("Tipo de avaria não encontrado.");
            }

            tipoAvaria.UpdateEntity(request.Tipo, request.Descricao, request.Status);

            await _tipoAvariaRepository.UpdateAsync(tipoAvaria, cancellationToken);
            return Result<Guid>.Success(request.Id);
        }
    }
}