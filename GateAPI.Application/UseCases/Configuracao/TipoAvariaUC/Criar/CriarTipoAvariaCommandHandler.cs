using GateAPI.Application.Common.Models;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Criar
{
    public class CriarTipoAvariaCommandHandler : IRequestHandler<CriarTipoAvariaCommand, Result<Guid>>
    {
        public readonly ITipoAvariaRepository _tipoAvariaRepository;

        public CriarTipoAvariaCommandHandler(ITipoAvariaRepository tipoAvariaRepository)
        {
            _tipoAvariaRepository = tipoAvariaRepository;
        }

        public async Task<Result<Guid>> Handle(CriarTipoAvariaCommand request, CancellationToken cancellationToken)
        {
            var result = await _tipoAvariaRepository.AddAsync(request.TipoAvaria, cancellationToken);
            return Result<Guid>.Success(result.Id);
        }
    }
}