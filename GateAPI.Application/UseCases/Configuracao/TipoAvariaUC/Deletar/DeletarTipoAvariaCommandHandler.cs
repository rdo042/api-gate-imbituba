
using GateAPI.Application.Common.Models;
using GateAPI.Application.UseCases.Configuracao.TipoLacreUC.Deletar;
using GateAPI.Domain.Repositories.Configuracao;
using MediatR;

namespace GateAPI.Application.UseCases.Configuracao.TipoAvariaUC.Deletar
{
    public class DeletarTipoAvariaCommandHandler: IRequestHandler<DeletarTipoAvariaCommand, Result<Guid>>
    {
        private readonly ITipoAvariaRepository _tipoAvariaRepository;

        public DeletarTipoAvariaCommandHandler(ITipoAvariaRepository tipoAvariaRepository)
        {
            _tipoAvariaRepository = tipoAvariaRepository;
        }

        public async Task<Result<Guid>> Handle(DeletarTipoAvariaCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _tipoAvariaRepository.DeleteAsync(command.Id, cancellationToken);

            if (!result)
                return Result<Guid>.Failure("Tipo de avaria não encontrado para exclusão.");

            return Result<Guid>.Success(command.Id);

        }
    }
}
