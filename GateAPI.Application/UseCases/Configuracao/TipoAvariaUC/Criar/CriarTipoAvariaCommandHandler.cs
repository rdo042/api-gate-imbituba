using GateAPI.Application.Common.Models;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Criar
{
    public class CriarTipoAvariaCommandHandler : IRequestHandler<CriarTipoAvariaCommand, Result<TipoAvaria>>
    {
        public readonly ITipoAvariaRepository _tipoAvariaRepository;

        public CriarTipoAvariaCommandHandler(ITipoAvariaRepository tipoAvariaRepository)
        {
            _tipoAvariaRepository = tipoAvariaRepository;
        }

        public async Task<Result<TipoAvaria>> Handle(CriarTipoAvariaCommand request, CancellationToken cancellationToken)
        {
            var tipoAvaria = new TipoAvaria(request.Tipo, request.Descricao, request.Status);

            var result = await _tipoAvariaRepository.AddAsync(tipoAvaria, cancellationToken);
            return Result<TipoAvaria>.Success(result);
        }
    }
}