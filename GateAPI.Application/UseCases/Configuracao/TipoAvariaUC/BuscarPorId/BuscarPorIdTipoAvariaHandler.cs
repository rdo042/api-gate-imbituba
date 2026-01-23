using GateAPI.Application.Common.Models;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC
{
    public class BuscarPorIdTipoAvariaHandler : IRequestHandler<BuscarPorIdTipoAvariaQuery, Result<Domain.Entities.Configuracao.TipoAvaria?>>
    {
        private readonly ITipoAvariaRepository _tipoAvariaRepository;
        public BuscarPorIdTipoAvariaHandler(ITipoAvariaRepository tipoAvariaRepository)
        {
            _tipoAvariaRepository = tipoAvariaRepository;
        }

        public async Task<Result<Domain.Entities.Configuracao.TipoAvaria?>> Handle(BuscarPorIdTipoAvariaQuery request, CancellationToken cancellationToken)
        {
            var tipoAvaria = await _tipoAvariaRepository.GetByIdAsync(request.Id);

            if (tipoAvaria is null)
                return Result<Domain.Entities.Configuracao.TipoAvaria?>.Failure($"Tipo de avaria não encontrado {request.Id}");

            return Result<Domain.Entities.Configuracao.TipoAvaria?>.Success(tipoAvaria);
        }
    }
}
