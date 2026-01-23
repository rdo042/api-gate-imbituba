using GateAPI.Application.Common.Models;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.AtualizarParcial
{
    public class AtualizarParcialTipoAvariaCommandHandler : IRequestHandler<AtualizarParcialTipoAvariaCommand, Result<Guid>>
    {
        public readonly ITipoAvariaRepository _tipoAvariaRepository;

        public AtualizarParcialTipoAvariaCommandHandler(ITipoAvariaRepository tipoAvariaRepository)
        {
            _tipoAvariaRepository = tipoAvariaRepository;
        }

        public async Task<Result<Guid>> Handle(AtualizarParcialTipoAvariaCommand request, CancellationToken cancellationToken)
        {
            
            var tipoAvaria = await _tipoAvariaRepository.GetByIdAsync(request.Id, cancellationToken);
            if (tipoAvaria is null)
            {
                return Result<Guid>.Failure("Tipo de avaria não encontrado.");
            }


            if(request.Status != null) tipoAvaria.SetStatus((StatusEnum)request.Status);
            if (!string.IsNullOrWhiteSpace(request.Descricao)) tipoAvaria.SetDescricao(request.Descricao);
            if (!string.IsNullOrWhiteSpace(request.Tipo)) tipoAvaria.SetTipo(request.Tipo);

            await _tipoAvariaRepository.UpdateAsync(tipoAvaria, cancellationToken);
            return Result<Guid>.Success(request.Id);
        }
    }
}