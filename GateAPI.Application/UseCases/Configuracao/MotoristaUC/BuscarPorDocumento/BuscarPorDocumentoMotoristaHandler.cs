using GateAPI.Application.Common.Models;
using GateAPI.Application.UseCases.Configuracao.MotoristaUC.BuscarTodos;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC
{
    public class BuscarPorDocumentosMotoristaHandler : IRequestHandler<BuscarPorDocumentoMotoristaQuery, Result<Motorista?>>
    {
        private readonly IMotoristaRepository _motoristaRepository;
        public BuscarPorDocumentosMotoristaHandler(IMotoristaRepository motoristaRepository)
        {
            _motoristaRepository = motoristaRepository;
        }

        public async Task<Result<Motorista?>> Handle(BuscarPorDocumentoMotoristaQuery request, CancellationToken cancellationToken)
        {
            var motorista = await _motoristaRepository.GetByDocumentoAsync(request.documento, cancellationToken);

            if (motorista is null)
                return Result<Motorista?>.Failure($"Motorista não encontrado {request.documento}");
           
            return Result<Motorista?>.Success(motorista);
        }
    }
}
